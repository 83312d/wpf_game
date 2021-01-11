
namespace Core.Models
{
    public class Monster : LivingBeing
    {
        public string Picture { get; }
        public int RewardXp { get; }
        
        public Monster(string name, string picture, int maxHitPoints, int hitPoints, int rewardXp, int rewardHairballs)
            : base(name, maxHitPoints, hitPoints, rewardHairballs)
        {
            Picture = $"/Core;component/Art/Monsters/{picture}";
            RewardXp = rewardXp;
        }
    }
}