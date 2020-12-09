namespace Core.Models
{
    public class Item
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Unique { get; set; }

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