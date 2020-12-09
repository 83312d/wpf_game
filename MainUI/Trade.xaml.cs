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
            if (((FrameworkElement)sender).DataContext is GroupedInventory groupedInventory)
            {
                Session.CurrentPlayer.LoseHairballs(groupedInventory.Item.Price);
                Session.CurrentTrader.AddItemToInventory(groupedInventory.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(groupedInventory.Item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is GroupedInventory groupedInventory)
            {
                if (Session.CurrentPlayer.Hairballs >= groupedInventory.Item.Price)
                {
                    Session.CurrentPlayer.RecieveHairballs(groupedInventory.Item.Price);
                    Session.CurrentPlayer.AddItemToInventory(groupedInventory.Item);
                    Session.CurrentTrader.RemoveItemFromInventory(groupedInventory.Item);
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