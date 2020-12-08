using System;

namespace Core.Models
{
    public class QuestStatus
    {
        public Quest CurrentQuest { get; set; }
        public bool IsComplete { get; set; }

        public QuestStatus(Quest quest)
        {
            CurrentQuest = quest;
            IsComplete = false;
        }
    }
}