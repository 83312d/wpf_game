namespace Core.Models
{
    public class Item
    {
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool Unique { get; }

        public Item(int itemTypeId, string name, int price, bool unique = false)
        {
            ItemTypeID = itemTypeId;
            Name = name;
            Price = price;
            Unique = unique;
        }

        public Item Clone()
        {
            return new Item(ItemTypeID, Name, Price);
        }
    }
}