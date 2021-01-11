using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using Core.EventArgs;
using Core.Models;
using Core.Factories;

namespace Core.ViewModels
{
    public class GameSession : AbstractNotifyClass
    {
        public event EventHandler<MessagesEventArgs> OnMessageRaised;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;
        private Player _currentPlayer;
        public World World { get; }
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLevelUp -= OnCurrentPlayerLevelUp;
                    _currentPlayer.OnDefeat -= OnCurrentPlayerDefeat;
                    _currentPlayer.OnActionDo -= OnCurrentPlayerDoAction;
                }

                _currentPlayer = value;
                
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLevelUp += OnCurrentPlayerLevelUp;
                    _currentPlayer.OnDefeat += OnCurrentPlayerDefeat;
                    _currentPlayer.OnActionDo += OnCurrentPlayerDoAction;
                }
            }
        }

        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;    
        public bool HasNorthLocation => World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis + 1) != null;
        public bool HasEastLocation => World.LocationAt(CurrentLocation.XAxis + 1, CurrentLocation.YAxis) != null;
        public bool HasWestLocation => World.LocationAt(CurrentLocation.XAxis - 1, CurrentLocation.YAxis) != null;
        public bool HasSouthLocation => World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis - 1) != null;

        public enum Directions
        {
            North,
            West,
            East,
            South
        }

        public GameSession()
        {
            World = WorldFactory.CreateWorld();
            CurrentPlayer = SetPlayer();
            CurrentLocation = World.LocationAt(0, 0);
        }
        
        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNorthLocation));
                OnPropertyChanged(nameof(HasEastLocation));
                OnPropertyChanged(nameof(HasWestLocation));
                OnPropertyChanged(nameof(HasSouthLocation));

                QuestAtLocation();
                CompleteQuestsAtLocation();
                MonstersAtLocation();
                
                CurrentTrader = CurrentLocation.Trader;
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnDefeat -= OnCurrentMonsterDefeat;
                }
                
                _currentMonster = value;

                if (CurrentMonster != null)
                {
                    _currentMonster.OnDefeat += OnCurrentMonsterDefeat;
                    RaiseMessage("");
                    RaiseMessage("It's a trap!");
                    RaiseMessage($"{CurrentMonster.Name} here!");
                }
                
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }
        
        public Trader CurrentTrader
        {
            get => _currentTrader;
            set
            {
                _currentTrader = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    if (HasNorthLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis + 1);
                    }
                    break;
                case Directions.East:
                    if (HasEastLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis + 1, CurrentLocation.YAxis);
                    }
                    break;
                case Directions.West:
                    if (HasWestLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis - 1, CurrentLocation.YAxis);
                    }
                    break;
                case Directions.South:
                    if (HasSouthLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis - 1);
                    }
                    break;
            }
        }
        
        private static Player SetPlayer()
        {
            var player = new Player(
                "Shafto",
                "Pirate",
                0,
                10,
                10,
                100
            );
            
            player.AddItemToInventory(LootFactory.CreateLoot(1001));

            return player;
        }

        private void QuestAtLocation()
        {
            foreach (var quest in CurrentLocation.AvailableQuests)
            {
                if (CurrentPlayer.Quests.All(q => q.CurrentQuest.Id != quest.Id))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }    
            }
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (var quest in CurrentLocation.AvailableQuests)
            {
                QuestStatus questToComplete = CurrentPlayer.Quests.FirstOrDefault(
                    q => q.CurrentQuest.Id == quest.Id && !q.IsComplete);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllNeededItems(quest.ItemsForCompletion))
                    {
                        foreach (var itemQuantity in quest.ItemsForCompletion)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => 
                                    item.ItemId == itemQuantity.ItemID));
                            }
                        }
                        
                        RaiseMessage("");
                        RaiseMessage("Hooray! You fulfilled your destiny!");
                        RaiseMessage($"Epic quest \"{quest.Name}\" complete!");
                        RaiseMessage($"You receive {quest.RewardXP} experience points");
                        RaiseMessage($"You receive {quest.RewardHairballs} hairballs");
                        
                        CurrentPlayer.AddXp(quest.RewardXP);
                        CurrentPlayer.ReceiveHairballs(quest.RewardHairballs);
                        
                        foreach (var itemQuantity in quest.RewardLoot)
                        {
                            Item item = LootFactory.CreateLoot(itemQuantity.ItemID);
                            
                            CurrentPlayer.AddItemToInventory(item);
                            RaiseMessage($"You receive {item.Name}");
                        }

                        questToComplete.IsComplete = true;
                    }
                }
            }
        }

        public void AttackCurrentMonster()
        {
            if (CurrentPlayer.CurrentWeapon == null)
            {
                RaiseMessage("Nothing to attack with! :(");
                return;
            }

            CurrentPlayer.UseCurrentWeapon(CurrentMonster);

            if (CurrentMonster.Defeated)
            {
                MonstersAtLocation();
            }
            else
            {
                int damageToPlayer = GodOfRandom.NumberBetween(CurrentMonster.MinDamage, CurrentMonster.MaxDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} misses you! Wow you are so fast!");
                }
                else
                {
                    RaiseMessage($"{CurrentMonster.Name} hits you for {damageToPlayer} points");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
            }
        }

        private void OnCurrentPlayerDefeat(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage("Oh, no!");
            if(CurrentMonster != null) RaiseMessage($"The {CurrentMonster.Name} defeats you...");
           
            CurrentLocation = World.LocationAt(0, -1);
            CurrentPlayer.Heal(CurrentPlayer.MaxHitPoints);
            
            if (CurrentPlayer.Hairballs < 0)
            {
                int lostHairballs = GodOfRandom.NumberBetween(0, CurrentPlayer.Hairballs);
                RaiseMessage($"You lose {lostHairballs} hairballs :(");
                CurrentPlayer.LoseHairballs(lostHairballs);
            }
        }
        
        private void OnCurrentMonsterDefeat(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage("Victorious!");
            RaiseMessage($"You defeated the {CurrentMonster.Name}");
            RaiseMessage($"You received {CurrentMonster.RewardXp} experience");
            RaiseMessage($"You also find {CurrentMonster.Hairballs} hairballs!");
            
            CurrentPlayer.AddXp(CurrentMonster.RewardXp);
            CurrentPlayer.ReceiveHairballs(CurrentMonster.Hairballs);

            foreach (var item in CurrentMonster.Inventory)
            {
                RaiseMessage($"You received {item.Name}");
                CurrentPlayer.AddItemToInventory(item);
            }
        }

        private void OnCurrentPlayerLevelUp(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage("Your might has grown!");
            RaiseMessage($"You are level {CurrentPlayer.Level} now!");
        }
        
        private void MonstersAtLocation() 
            => CurrentMonster = CurrentLocation.GetMonster();

        private void RaiseMessage(string message) 
            => OnMessageRaised?.Invoke(this, new MessagesEventArgs(message));
        
        private void OnCurrentPlayerDoAction(object sender, string result) 
            => RaiseMessage(result);
    }
}