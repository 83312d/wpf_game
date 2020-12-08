using System;

namespace Core.Models
{
    public class QuestStatus : BaseClass
    {
        public Quest CurrentQuest { get; set; }
        private bool _isComplete;

        public bool IsComplete
        {
            get => _isComplete;
            set
            {
                _isComplete = value;
                OnPropertyChanged(nameof(IsComplete));
            }
        }

        public QuestStatus(Quest quest)
        {
            CurrentQuest = quest;
            IsComplete = false;
        }
    }
}