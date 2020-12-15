
namespace Core.Models
{
    public class Monster : LivingBeing
    {
        public string Picture { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }
        public int RewardXp { get; }
        
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