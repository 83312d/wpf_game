using System.Collections.Generic;

namespace Core.Models
{
    public class World
    {
        private List<Location> locations = new List<Location>();

        internal void AddLocation (int xAxis, int yAxis, string name, string description, string picture)
        {
            Location somePlace = new Location();
            somePlace.XAxis = xAxis;
            somePlace.YAxis = yAxis;
            somePlace.Name = name;
            somePlace.Description = description;
            somePlace.Picture = picture;
            
            locations.Add(somePlace);
        }

        public Location LocationAt (int xAxis, int yAxis)
        {
            foreach (var location in locations)
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