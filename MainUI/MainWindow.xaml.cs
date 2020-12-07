using System.Windows;
using Core.ViewModels;

namespace WPF_GAME
{
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession;

        public MainWindow()
        {
            InitializeComponent();
            
            _gameSession = new GameSession();
            
            DataContext = _gameSession;
        }

        private void OnClick_GoNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.Move(GameSession.Directions.North);
        }

        private void OnClick_GoWest(object sender, RoutedEventArgs e)
        {
            _gameSession.Move(GameSession.Directions.West);
        }

        private void OnClick_GoEast(object sender, RoutedEventArgs e)
        {
            _gameSession.Move(GameSession.Directions.East);
        }

        private void OnClick_GoSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.Move(GameSession.Directions.South);
        }
    }
}
