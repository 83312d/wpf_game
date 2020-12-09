using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Models
{
    public class Player : LivingBeing
    {
        private string _characterClass;
        private int _level;
        private int _xPoints;

        public string CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int ExperiencePoints
        {
            get => _xPoints;
            set
            {
                _xPoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public ObservableCollection<QuestStatus> Quests { get; set; }

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
                    i.ItemTypeID == item.ItemID) >= item.Quantity);
        }
    }
}