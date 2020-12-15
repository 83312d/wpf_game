namespace Core.Models
{
    public class MonsterEncounter
    {
        public int MonsterId { get; }
        public int ChanceOfEncounter { get; set; }

        public MonsterEncounter(int monsterId, int chanceOfEncounter)
        {
            MonsterId = monsterId;
            ChanceOfEncounter = chanceOfEncounter;
        }
    }
}