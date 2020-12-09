using System.Windows;
using Core.Models;
using Core.ViewModels;

namespace MainUI
{
    public partial class Trade : Window
    {
        public GameSession Session => DataContext as GameSession;
        
        public Trade()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            Item item = ((FrameworkElement)sender).DataContext as Item;

            if (item != null)
            {
                Session.CurrentPlayer.Hairballs += item.Price;
                Session.CurrentTrader.AddItemToInventory(item);
                Session.CurrentPlayer.RemoveItemFromInventory(item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            Item item = ((FrameworkElement)sender).DataContext as Item;

            if (item != null)
            {
                if (Session.CurrentPlayer.Hairballs >= item.Price)
                {
                    Session.CurrentPlayer.Hairballs -= item.Price;
                    Session.CurrentPlayer.AddItemToInventory(item);
                    Session.CurrentTrader.RemoveItemFromInventory(item);
                }
                else
                {
                    MessageBox.Show("Not enough hairballs");
                }
                
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}