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
                    var pigeon = new Monster(
                        "Evil Pigeon", 
                        "EvilPigeon.png", 
                        4, 4, 
                        5, 1
                    );

                    pigeon.CurrentWeapon = LootFactory.CreateLoot(2001);

                    AddLoot(pigeon, 0001, 75);
                    AddLoot(pigeon, 0002, 25);

                    return pigeon;

                case 2:
                    var spirit = new Monster(
                        "Spirit",
                        "Spirit.png",
                        6, 6, 
                        10, 2
                    );

                    spirit.CurrentWeapon = LootFactory.CreateLoot(2002);

                    AddLoot(spirit, 0003, 75);
                    AddLoot(spirit, 0004, 20);

                    return spirit;

                case 3:
                    var elevator = new Monster(
                        "Elevator",
                        "Elevator.png", 
                        20, 20, 
                        20, 5
                    );

                    elevator.CurrentWeapon = LootFactory.CreateLoot(2003);

                    AddLoot(elevator, 0005, 70);
                    AddLoot(elevator, 0006, 10);

                    return elevator;

                default:
                    throw new ArgumentException(string.Format($"Monster type '{monsterId}' does not exist"));
            }
        }

        private static void AddLoot(Monster monster, int itemId, int chance)
        {
            if (GodOfRandom.NumberBetween(1, 100) <= chance)
            {
                monster.AddItemToInventory(LootFactory.CreateLoot(itemId));
            }
        }
    }
}