using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            List<ItemQuantity> itemsToCompletion = new List<ItemQuantity>();
            List<ItemQuantity> rewardLoot = new List<ItemQuantity>();
            
            itemsToCompletion.Add(new ItemQuantity(0001, 5));
            rewardLoot.Add(new ItemQuantity(1002, 1));
            
            _quests.Add(new Quest(
                    1,
                    "Defend boxes from pigeon squatters",
                    "I must defend even abandoned property! To Battle!",
                    itemsToCompletion,
                    25, 
                    10,
                    rewardLoot
                )
            );
        }
        
        internal static Quest GetQuestById(int id)
        {
            return _quests.FirstOrDefault(quest => quest.Id == id);
        }
    }
}