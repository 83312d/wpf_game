namespace Core.Models
{
    public class GroupedInventory : AbstractNotifyClass
    {
        private Item _item;
        private int _quantity;

        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public GroupedInventory(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}