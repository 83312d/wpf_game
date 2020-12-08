using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.Factories
{
    public static class LootFactory
    {
        private static readonly List<Item> StandartLoot = new List<Item>();

        static LootFactory()
        {
            StandartLoot.Add(new Weapon(1001, "Blunt Claws", 1, 1, 2));
            StandartLoot.Add(new Weapon(1002, "Toothpick", 5, 2, 4));
            StandartLoot.Add(new Weapon(1003, "Sharp Claws", 10, 4, 8));
            StandartLoot.Add(new Weapon(1004, "Mighty KYCb", 50, 10, 20));
            StandartLoot.Add(new Item(0002, "Pigeon Fang", 5));
            StandartLoot.Add(new Item(0001, "Pigeon Feather", 1));
            StandartLoot.Add(new Item(0004, "Spirit Essence", 10));
            StandartLoot.Add(new Item(0003, "Spirit Ectoplasm", 2));
            StandartLoot.Add(new Item(0006, "Elevator Engine", 50));
            StandartLoot.Add(new Item(0005, "Elevator Door Claw", 10));
        }

        public static Item CreateLoot(int itemTypeId)
        {
            Item standartItem = StandartLoot.FirstOrDefault(item => item.ItemTypeID == itemTypeId);

            if (standartItem != null)
            {
                return standartItem is Weapon weapon ? weapon.Clone() : standartItem.Clone();
            }

            return null;
        }
    }
}