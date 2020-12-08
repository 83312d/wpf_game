using System.Collections.Generic;

namespace Core.Models
{
    public class Location
    {
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public List<Quest> AvailableQuests { get; set; } = new List<Quest>();
    }
}