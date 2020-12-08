namespace Core.Models
{
    public class MonsterEncounter
    {
        public int MonsterId { get; set; }
        public int ChanceOfEncounter { get; set; }

        public MonsterEncounter(int monsterId, int chanceOfEncounter)
        {
            MonsterId = monsterId;
            ChanceOfEncounter = chanceOfEncounter;
        }
    }
}