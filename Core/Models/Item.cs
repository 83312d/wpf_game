namespace Core.Models
{
    public class Item
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Item(int itemTypeId, string name, int price)
        {
            ItemTypeID = itemTypeId;
            Name = name;
            Price = price;
        }

        public Item Clone()
        {
            return new Item(ItemTypeID, Name, Price);
        }
    }
}