using System.Collections.Generic;
using System.Linq;
using Core.Factories;

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
        public List<MonsterEncounter> Monsters { get; set; } = new List<MonsterEncounter>();

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