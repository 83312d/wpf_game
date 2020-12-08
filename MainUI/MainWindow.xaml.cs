using System.Windows;
using System.Windows.Documents;
using Core.EventArgs;
using Core.ViewModels;

namespace WPF_GAME
{
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new GameSession();

        public MainWindow()
        {
            InitializeComponent();
            
            _gameSession.OnMessageRaised += GameMessageRaised;
            
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

        private void GameMessageRaised(object sender, MessagesEventArgs e)
        {
            Messages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            Messages.ScrollToEnd();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }
    }
}
