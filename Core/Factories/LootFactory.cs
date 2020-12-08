using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.Factories
{
    public static class LootFactory
    {
        private static List<Item> _standartLoot;

        static LootFactory()
        {
            _standartLoot = new List<Item>();
            
            _standartLoot.Add(new Weapon(1001, "Blunt Claws", 1, 1, 1));
            _standartLoot.Add(new Weapon(1002, "Toothpick", 5, 1, 3));
            _standartLoot.Add(new Item(0002, "Pigeon Fang", 5));
            _standartLoot.Add(new Item(0001, "Pigeon Feather", 1));
            _standartLoot.Add(new Item(0004, "Spirit Essence", 10));
            _standartLoot.Add(new Item(0003, "Spirit Ectoplasm", 2));
            _standartLoot.Add(new Item(0006, "Elevator Engine", 50));
            _standartLoot.Add(new Item(0005, "Elevator Door Claw", 10));
        }

        public static Item CreateLoot(int itemTypeId)
        {
            Item standartItem = _standartLoot.FirstOrDefault(item => item.ItemTypeID == itemTypeId);

            if (standartItem != null)
            {
                return standartItem.Clone();
            }

            return null;
        }
    }
}