
namespace Core.Models
{
    public class Trader : LivingBeing
    {
        public int Id { get; set; }
        
        public Trader(string name, int id) : base(name, 9999, 9999, 9999)
        {
            Id = id;
        }
    }
}