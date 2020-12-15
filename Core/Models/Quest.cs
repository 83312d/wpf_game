using System.Collections.Generic;
using System.Security.Cryptography;

namespace Core.Models
{
    public class Quest
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        
        public List<ItemQuantity> ItemsForCompletion { get; }
        
        public int RewardXP { get; }
        public int RewardHairballs { get; }
        public List<ItemQuantity> RewardLoot { get; }

        public Quest(int id, string name, string description, List<ItemQuantity> itemsForCompletion, 
            int rewardXp, int rewardHairballs, List<ItemQuantity> rewardLoot) 
        {
            Id = id;
            Name = name;
            Description = description;
            ItemsForCompletion = itemsForCompletion;
            RewardHairballs = rewardHairballs;
            RewardLoot = rewardLoot;
            RewardXP = rewardXp;
        }
    }
}