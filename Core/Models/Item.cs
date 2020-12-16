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
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public Item(ItemCategory category, int itemId, string name, int price, 
                    bool unique = false, int minDamage = 0, int maxDamage = 0)
        {
            Category = category;
            ItemId = itemId;
            Name = name;
            Price = price;
            Unique = unique;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public Item Clone()
        {
            return new Item(Category, ItemId, Name, Price, Unique, MinDamage, MaxDamage);
        }
    }
}