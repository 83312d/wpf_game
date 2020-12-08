using System.Collections.ObjectModel;

namespace Core.Models
{
    public class Monster : BaseClass
    {
        private int _hitPoints;
        public string Name { get; private set; }
        public string Picture { get; set; }
        public int MaxHitPoints { get; private set; }
        public int HitPoints
        {
            get => _hitPoints;
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int RewardXP { get; private set; }
        public int RewardGold { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }
        
        public Monster(string name, string picture, int maxHitPoints, int hitPoints, int rewardXp, int rewardGold)
        {
            Name = name;
            Picture = picture;
            MaxHitPoints = maxHitPoints;
            HitPoints = hitPoints;
            RewardGold = rewardGold;
            RewardXP = rewardXp;
            
            Inventory = new ObservableCollection<ItemQuantity>();
        }
    }
}