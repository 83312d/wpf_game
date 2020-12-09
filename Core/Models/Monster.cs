
namespace Core.Models
{
    public class Monster : LivingBeing
    {
        public string Picture { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int RewardXp { get; private set; }
        
        public Monster(string name, string picture, int maxHitPoints, int hitPoints, int rewardXp, int rewardHairballs, int minDamage, int maxDamage)
            : base(name, maxHitPoints, hitPoints, rewardHairballs)
        {
            Picture = $"/Core;component/Art/Monsters/{picture}";
            RewardXp = rewardXp;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}