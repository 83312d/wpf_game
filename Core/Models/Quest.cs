using System.Collections.Generic;
using System.Security.Cryptography;

namespace Core.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<ItemQuantity> ItemsForCompletion { get; set; }
        
        public int RewardXP { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardLoot { get; set; }

        public Quest(int id, string name, string description, List<ItemQuantity> itemsForCompletion, 
            int rewardXp, int rewardGold, List<ItemQuantity> rewardLoot) 
        {
            Id = id;
            Name = name;
            Description = description;
            ItemsForCompletion = itemsForCompletion;
            RewardGold = rewardGold;
            RewardLoot = rewardLoot;
            RewardXP = rewardXp;
        }
    }
}