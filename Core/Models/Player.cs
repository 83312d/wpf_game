using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Models
{
    public class Player : LivingBeing
    {
        private string _characterClass;
        private int _xPoints;

        public string CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }
        public int ExperiencePoints
        {
            get => _xPoints;
            private set
            {
                _xPoints = value;
                OnPropertyChanged();
                SetLevel();
            }
        }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public event EventHandler OnLevelUp;

        public Player(string name, string characterClass, int xPoints, int maxHitPoints, int hitPoints, int hairballs) 
            : base(name, maxHitPoints, hitPoints, hairballs)
        {
            CharacterClass = characterClass;
            ExperiencePoints = xPoints;
            Quests = new ObservableCollection<QuestStatus>();
        }

        public bool HasAllNeededItems(List<ItemQuantity> items)
        {
            return items.All(item => 
                Inventory.Count(i => 
                    i.ItemId == item.ItemID) >= item.Quantity);
        }

        public void AddXp(int amount)
        {
            ExperiencePoints += amount;
        }

        public void SetLevel()
        {
            int currentLevel = Level;
            Level = (ExperiencePoints / 100) + 1;

            if (Level != currentLevel)
            {
                MaxHitPoints = Level * 10;
                Heal(MaxHitPoints);

                OnLevelUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}