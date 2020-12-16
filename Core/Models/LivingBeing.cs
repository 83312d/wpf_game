using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Models
{
    public abstract class LivingBeing : AbstractNotifyClass
    {
        private string _name;
        private int _currentHitPoints;
        private int _maxHitPoints;
        private int _hairballs;
        private int _level;

        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public int Hairballs
        {
            get => _hairballs;
            private set
            {
                _hairballs = value;
                OnPropertyChanged();
            }
        }
        public int CurrentHitPoints
        {
            get => _currentHitPoints;
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }
        public int MaxHitPoints
        {
            get => _maxHitPoints;
            protected set
            {
                _maxHitPoints = value;
                OnPropertyChanged();
            }
        }
        public int Level
        {
            get => _level;
            protected set
            {
                _level = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Item> Inventory { get; }
        public ObservableCollection<GroupedInventory> GroupedInventory { get; }
        public List<Item> Weapons 
            => Inventory.Where(i => i.Category == Item.ItemCategory.Weapon).ToList();
        public bool Defeated => CurrentHitPoints <= 0;
        public event EventHandler OnDefeat;

        protected LivingBeing(string name, int maxHitPoints, int currentHitPoints, int hairballs, int level = 1)
        {
            Name = name;
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = currentHitPoints;
            Hairballs = hairballs;
            Level = level;
            Inventory = new ObservableCollection<Item>();
            GroupedInventory = new ObservableCollection<GroupedInventory>();
        }

        public void TakeDamage(int damage)
        {
            CurrentHitPoints -= damage;

            if (Defeated)
            {
                CurrentHitPoints = 0;
                RaiseOnDeathEvent();
            }
        }

        public void Heal(int amount)
        {
            if (CurrentHitPoints + amount >= MaxHitPoints)
            {
                CurrentHitPoints = MaxHitPoints;
            }
            else
            {
                CurrentHitPoints += amount;
            }
        }

        public void RecieveHairballs(int amount)
        {
            Hairballs += amount;
        }

        public void LoseHairballs(int amount)
        {
            if (amount > Hairballs)
            {
                throw new ArgumentOutOfRangeException($"You only have {Hairballs} hairballs!");
            }
            
            Hairballs -= amount;
        }
        
        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);

            if (item.Unique)
            {
                GroupedInventory.Add(new GroupedInventory(item, 1));
            }
            else
            {
                if (!GroupedInventory.Any(ig => ig.Item.ItemTypeID == item.ItemTypeID))
                {
                    GroupedInventory.Add(new GroupedInventory(item,0));
                }

                GroupedInventory.First(ig => ig.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
            
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(Item item)
        {
            Inventory.Remove(item);

            GroupedInventory itemToRemove = item.Unique
                ? GroupedInventory.FirstOrDefault(gi => gi.Item == item)
                : GroupedInventory.FirstOrDefault(gi => gi.Item.ItemTypeID == item.ItemTypeID);

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(itemToRemove);
                }
                else
                {
                    itemToRemove.Quantity--;
                }
            }
            
            OnPropertyChanged(nameof(Weapons));
        }

        private void RaiseOnDeathEvent()
        {
            OnDefeat?.Invoke(this, new System.EventArgs());
        }
    }
}