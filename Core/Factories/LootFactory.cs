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
            
            _standartLoot.Add(new Weapon(0001, "Blunt Claws", 1, 1, 1));
            _standartLoot.Add(new Weapon(0002, "Toothpick", 5, 1, 3));
            _standartLoot.Add(new Item(1002, "Pigeon Fang", 5));
            _standartLoot.Add(new Item(1001, "Pigeon Feather", 1));
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