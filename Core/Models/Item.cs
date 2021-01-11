using Core.Actions;

namespace Core.Models
{
    public class Item
    {
        public enum ItemCategory
        {
            Weapon,
            Potion,
            Misc,
            MonsterPart
        }
        
        public ItemCategory Category { get; }
        public int ItemId { get; }
        public string Name { get; }
        public int Price { get; }
        public bool Unique { get; }
        public IAction Action { get; set; }
        
        public Item(ItemCategory category, int itemId, string name, int price, 
                    bool unique = false, IAction action = null)
        {
            Category = category;
            ItemId = itemId;
            Name = name;
            Price = price;
            Unique = unique;
            Action = action;
        }

        public void ExecuteAction(LivingBeing actor, LivingBeing target)
        {
            Action?.Execute(actor, target);
        }

        public Item Clone()
        {
            return new Item(Category, ItemId, Name, Price, Unique, Action);
        }
    }
}