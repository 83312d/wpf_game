using System.Collections.ObjectModel;

namespace Core.Models
{
    public class Trader : BaseClass
    {
        public string Name { get; set; }
        public int Id { get; set; }
        
        public ObservableCollection<Item> Inventory { get; set; }

        public Trader(string name, int id)
        {
            Name = name;
            Id = id;
            Inventory = new ObservableCollection<Item>();
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        public void RemoveItemFromInventory(Item item)
        {
            Inventory.Remove(item);
        }
    }
}