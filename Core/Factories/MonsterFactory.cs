using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Core.Models;

namespace Core.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterId)
        {
            switch (monsterId)
            {
                case 1:
                    Monster pigeon = new Monster(
                        "Evil Pigeon", 
                        "/Core;component/Art/Monsters/EvilPigeon.png", 
                        4, 4, 5, 1
                    );

                    AddLoot(pigeon, 0001, 75);
                    AddLoot(pigeon, 0002, 25);

                    return pigeon;
                case 2:
                    Monster spirit = new Monster(
                        "Spirit",
                        "/Core;component/Art/Monsters/Spirit.png",
                        6, 6, 10, 2
                    );

                    AddLoot(spirit, 0003, 75);
                    AddLoot(spirit, 0004, 25);

                    return spirit;
                case 3:
                    Monster elevator = new Monster(
                        "Elevator",
                        "/Core;component/Art/Monsters/Elevator.png", 
                        20, 20, 20, 5
                    );

                    AddLoot(elevator, 0005, 75);
                    AddLoot(elevator, 0006, 25);

                    return elevator;
                default:
                    throw new ArgumentException(string.Format($"Monster type '{monsterId}' does not exist"));
            }
        }

        private static void AddLoot(Monster monster, int itemId, int chance)
        {
            if (GodOfRandom.NumberBetween(1, 100) <= chance)
            {
                monster.Inventory.Add(new ItemQuantity(itemId, 1));
            }
        }
    }
}