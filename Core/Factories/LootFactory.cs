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
            BuildWeapon(1001, "Blunt Claws", 1, 1, 2);
            BuildWeapon(1002, "Toothpick", 5, 2, 4);
            BuildWeapon(1003, "Sharp Claws", 10, 4, 8);
            BuildWeapon(1004, "Mighty KYCb", 50, 10, 20);
            BuildMonsterPart(0001, "Pigeon Feather", 1);
            BuildMonsterPart(0002, "Pigeon Fang", 5);
            BuildMonsterPart(0003, "Spirit Ectoplasm", 2);
            BuildMonsterPart(0004, "Spirit Essence", 10);
            BuildMonsterPart(0005, "Elevator Door Claw", 10);
            BuildMonsterPart(0006, "Elevator Engine", 50);
        }

        private static void BuildMonsterPart(int id, string name, int price)
        {
            StandartLoot.Add(new Item(Item.ItemCategory.Misc, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price,
                            int minDamage, int maxDamage)
        {
            StandartLoot.Add(new Item(Item.ItemCategory.Weapon, id, name, price, true, minDamage, maxDamage));
        }

        public static Item CreateLoot(int itemTypeId) => StandartLoot.FirstOrDefault(item => item.ItemTypeID == itemTypeId)?.Clone();

    }
}