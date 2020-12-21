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
        private Item _currentWeapon;

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
        public Item CurrentWeapon
        {
            get => _currentWeapon;
            set
            {
                if(_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionDo -= RaiseOnActionDoEvent;
                }
 
                _currentWeapon = value;
 
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionDo += RaiseOnActionDoEvent;
                }
 
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Item> Inventory { get; }
        public ObservableCollection<GroupedInventory> GroupedInventory { get; }
        public List<Item> Weapons 
            => Inventory.Where(i => i.Category == Item.ItemCategory.Weapon).ToList();
        public bool Defeated => CurrentHitPoints <= 0;
        public event EventHandler<string> OnActionDo;
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

        public void UseCurrentWeapon(LivingBeing target)
        {
            CurrentWeapon.DoAction(this, target);
        }

        public void TakeDamage(int damage)
        {
            CurrentHitPoints -= damage;

            if (!Defeated) return;
            
            CurrentHitPoints = 0;
            RaiseOnDefeatEvent();
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

        public void ReceiveHairballs(int amount)
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
                if(!GroupedInventory.Any(gi => gi.Item.ItemId == item.ItemId))
                {
                    GroupedInventory.Add(new GroupedInventory(item,0));
                }

                GroupedInventory.First(ig => ig.Item.ItemId == item.ItemId).Quantity++;
            }
            
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(Item item)
        {
            Inventory.Remove(item);

            GroupedInventory itemToRemove = item.Unique
                ? GroupedInventory.FirstOrDefault(gi => gi.Item == item)
                : GroupedInventory.FirstOrDefault(gi => gi.Item.ItemId == item.ItemId);

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

        private void RaiseOnDefeatEvent()
        {
            OnDefeat?.Invoke(this, new System.EventArgs());
        }

        private void RaiseOnActionDoEvent(object sender, string result)
        {
            OnActionDo?.Invoke(this, result);
        }
    }
}