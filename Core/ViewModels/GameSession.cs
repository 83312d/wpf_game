using Core.Models;
using Core.Factories;

namespace Core.ViewModels
{
    public class GameSession : BaseClass
    {
        private Location _currentLocation;
        public World World { get; set; }
        public Player CurrentPlayer { get; set; }

        public GameSession()
        {
            World = WorldFactory.CreateWorld();
            CurrentPlayer = SetPlayer();
            CurrentLocation = World.LocationAt(0, 0);
        }
        
        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasNorthLocation));
                OnPropertyChanged(nameof(HasEastLocation));
                OnPropertyChanged(nameof(HasWestLocation));
                OnPropertyChanged(nameof(HasSouthLocation));
            }
        }

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    if (HasNorthLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis + 1);
                    }
                    break;
                case Directions.East:
                    if (HasEastLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis + 1, CurrentLocation.YAxis);
                    }
                    break;
                case Directions.West:
                    if (HasWestLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis - 1, CurrentLocation.YAxis);
                    }
                    break;
                case Directions.South:
                    if (HasSouthLocation)
                    {
                        CurrentLocation = World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis - 1);
                    }
                    break;
            }
        }
        
        private static Player SetPlayer()
        {
            var player = new Player
                {
                    Name = "Shafto",
                    CharacterClass = "Pirate",
                    HitPoints = 10,
                    Gold = 100,
                    Level = 1,
                    ExperiencePoints = 0
                };
            
            return player;
        }
        
        public bool HasNorthLocation => World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis + 1) != null;
        public bool HasEastLocation => World.LocationAt(CurrentLocation.XAxis + 1, CurrentLocation.YAxis) != null;
        public bool HasWestLocation => World.LocationAt(CurrentLocation.XAxis - 1, CurrentLocation.YAxis) != null;
        public bool HasSouthLocation => World.LocationAt(CurrentLocation.XAxis, CurrentLocation.YAxis - 1) != null;

        public enum Directions
        {
           North,
           West,
           East,
           South
        }
    }
}