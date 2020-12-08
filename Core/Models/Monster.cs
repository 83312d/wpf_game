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
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int RewardXP { get; private set; }
        public int RewardHairballs { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }
        
        public Monster(string name, string picture, int maxHitPoints, int hitPoints, int rewardXp, int rewardHairballs, int minDamage, int maxDamage)
        {
            Name = name;
            Picture = $"/Core;component/Art/Monsters/{picture}";
            MaxHitPoints = maxHitPoints;
            HitPoints = hitPoints;
            RewardHairballs = rewardHairballs;
            RewardXP = rewardXp;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            
            Inventory = new ObservableCollection<ItemQuantity>();
        }
    }
}