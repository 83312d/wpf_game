using System;
using System.Linq;
using Core.EventArgs;
using Core.Models;
using Core.Factories;

namespace Core.ViewModels
{
    public class GameSession : BaseClass
    {
        public event EventHandler<MessagesEventArgs> OnMessageRaised;
        private Location _currentLocation;
        private Monster _currentMonster;
        public World World { get; set; }
        public Player CurrentPlayer { get; set; }
        public Weapon CurrentWeapon { get; set; }
        public bool HasMonster => CurrentMonster != null;
        public bool HasNorthLocation => World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis + 1) != null;

        public bool HasEastLocation => World.LocationAt(CurrentLocation.XAxis + 1, CurrentLocation.YAxis) != null;
        public bool HasWestLocation => World.LocationAt(CurrentLocation.XAxis - 1, CurrentLocation.YAxis) != null;
        public bool HasSouthLocation => World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis - 1) != null;

        public GameSession()
        {
            World = WorldFactory.CreateWorld();
            CurrentPlayer = SetPlayer();
            CurrentLocation = World.LocationAt(0, 0);

            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(LootFactory.CreateLoot(1001));
            }
        }
        
        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasNorthLocation));
                OnPropertyChanged(nameof(HasEastLocation));
                OnPropertyChanged(nameof(HasWestLocation));
                OnPropertyChanged(nameof(HasSouthLocation));

                QuestAtLocation();
                CompleteQuestsAtLocation();
                MonstersAtLocation();
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                _currentMonster = value;
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage("It's a trap!");
                    RaiseMessage($"{CurrentMonster.Name} here!");
                }
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
            var player = new Player
                {
                    Name = "Shafto",
                    CharacterClass = "Pirate",
                    HitPoints = 10,
                    Hairballs = 100,
                    Level = 1,
                    ExperiencePoints = 0
                };
            
            return player;
        }

        private void QuestAtLocation()
        {
            foreach (var quest in CurrentLocation.AvailableQuests)
            {
                if (!CurrentPlayer.Quests.Any(q => q.CurrentQuest.Id == quest.Id))
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
                                    item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }
                        
                        RaiseMessage("");
                        RaiseMessage("Hooray! You fulfilled your destiny!");
                        RaiseMessage($"Epic quest \"{quest.Name}\" complete!");

                        CurrentPlayer.ExperiencePoints += quest.RewardXP;
                        RaiseMessage($"You receive {quest.RewardXP} experience points");
                        CurrentPlayer.Hairballs += quest.RewardHairballs;
                        RaiseMessage($"You receive {quest.RewardHairballs} hairballs");

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

        private void MonstersAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new MessagesEventArgs(message));
        }

        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("Nothing to attack with! :(");
                return;
            }

            int damageToMonster = GodOfRandom.NumberBetween(CurrentWeapon.MinDamage, CurrentWeapon.MaxDamage);

            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed!");
            }
            else
            {
                CurrentMonster.HitPoints -= damageToMonster;
                RaiseMessage($"You hit the{CurrentMonster.Name} for {damageToMonster}");
            }

            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage("Victorious!");
                RaiseMessage($"You defeated the {CurrentMonster.Name}");

                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardXP;
                RaiseMessage($"You recived {CurrentMonster.RewardXP} expirience");

                CurrentPlayer.Hairballs += CurrentMonster.RewardHairballs;
                RaiseMessage($"You also find {CurrentMonster.RewardHairballs} hairballs!");

                foreach (var itemQuantity in CurrentMonster.Inventory)
                {
                    Item item = LootFactory.CreateLoot(itemQuantity.ItemID);
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You received {itemQuantity.Quantity} {item.Name}");
                }
                
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
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"{CurrentMonster.Name} hits you for {damageToPlayer} points");
                }

                if (CurrentPlayer.HitPoints <= 0)
                {
                    RaiseMessage("Oh, no!");
                    RaiseMessage($"The {CurrentMonster.Name} defeats you...");

                    CurrentLocation = World.LocationAt(0, -1);
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10;
                    
                    if (CurrentPlayer.Hairballs < 0)
                    {
                        int lostHairballs = GodOfRandom.NumberBetween(0, CurrentPlayer.Hairballs);
                        CurrentPlayer.Hairballs -= lostHairballs;
                        RaiseMessage($"You lose {lostHairballs} hairballs :(");
                    }
                }
            }
        }

        public enum Directions
        {
           North,
           West,
           East,
           South
        }
    }
}