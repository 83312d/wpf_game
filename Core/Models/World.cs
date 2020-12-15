using System.Collections.Generic;

namespace Core.Models
{
    public class World
    {
        private readonly List<Location> _locations = new List<Location>();

        internal void AddLocation(int xAxis, int yAxis, string name, string description, string picture)
        {
            string picturePath = $"/Core;component/Art/Locations/{picture}";

            _locations.Add(new Location(xAxis, yAxis, name, description, picturePath));
        }

        public Location LocationAt (int xAxis, int yAxis)
        {
            foreach (var location in _locations)
            {
                if (location.XAxis == xAxis && location.YAxis == yAxis)
                {
                    return location;
                }
            }
            
            return null;
        }
    }
}