using System.Collections.Generic;
using System.Linq;
using Core.Factories;

namespace Core.Models
{
    public class Location
    {
        public int XAxis { get; }
        public int YAxis { get; }
        public string Name { get; }
        public string Description { get; }
        public string Picture { get; }
        public List<Quest> AvailableQuests { get; } = new List<Quest>();
        public List<MonsterEncounter> Monsters { get; } = new List<MonsterEncounter>();
        public Trader Trader { get; set; }

        public Location(int xAxis, int yAxis, string name, string description, string picture)
        {
            XAxis = xAxis;
            YAxis = yAxis;
            Name = name;
            Description = description;
            Picture = picture;
        }

        public void AddMonster(int monsterId, int chanceOfEncounter)
        {
            if (Monsters.Exists(m => m.MonsterId == monsterId))
            {
                Monsters.First(m => m.MonsterId == monsterId)
                    .ChanceOfEncounter = chanceOfEncounter;
            }
            else
            {
                Monsters.Add(new MonsterEncounter(monsterId, chanceOfEncounter));
            }
        }

        public Monster GetMonster()
        {
            if (!Monsters.Any())
            {
                return null;
            }

            int totalChance = Monsters.Sum(m => m.ChanceOfEncounter);

            int randomNumber = GodOfRandom.NumberBetween(1, totalChance);

            int runningTotal = 0;
            foreach (var monsterEncounter in Monsters)
            {
                runningTotal += monsterEncounter.ChanceOfEncounter;
                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterId);
                }
            }
            
            //fail check
            return MonsterFactory.GetMonster(Monsters.Last().MonsterId);
        }
    }
}