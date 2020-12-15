using System;

namespace Core.Models
{
    public class QuestStatus : AbstractNotifyClass
    {
        public Quest CurrentQuest { get; }
        private bool _isComplete;

        public bool IsComplete
        {
            get => _isComplete;
            set
            {
                _isComplete = value;
                OnPropertyChanged();
            }
        }

        public QuestStatus(Quest quest)
        {
            CurrentQuest = quest;
            IsComplete = false;
        }
    }
}