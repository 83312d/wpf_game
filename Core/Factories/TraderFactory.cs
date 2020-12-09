using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.Factories
{
    public class TraderFactory
    {
        private static readonly List<Trader> Traders = new List<Trader>();

        static TraderFactory()
        {
            Trader samtvsung = new Trader("Sam TeVi Sung", 1);
            samtvsung.AddItemToInventory(LootFactory.CreateLoot(1002));
            
            Trader meatkeeper = new Trader("Meat Keeper", 2);
            meatkeeper.AddItemToInventory(LootFactory.CreateLoot(1003));

            Trader chinchilla = new Trader("Besya The Chinchilla", 3);
            chinchilla.AddItemToInventory(LootFactory.CreateLoot(1004));

            AddTrader(samtvsung);
            AddTrader(meatkeeper);
            AddTrader(chinchilla);
        }

        public static Trader GetTraderByName(string name)
        {
            return Traders.FirstOrDefault(t => t.Name == name);
        }

        public static Trader GetTraderById(int id)
        {
            return Traders.FirstOrDefault(t => t.Id == id);
        }

        private static void AddTrader(Trader trader)
        {
            if (Traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"Trader {trader.Name} already exists");
            }
            
            Traders.Add(trader);
        }
    }
}