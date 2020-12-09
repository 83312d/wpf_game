using Core.Models;

namespace Core.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            
            newWorld.AddLocation(
                0, 
                -1, 
                "Home", 
                "Precious paper palace!", 
                "Home.png"
            );
            
            newWorld.AddLocation(
                -1, 
                -1, 
                "Machines Domain", 
                "Strange place populated by Computers, Consoles and TV Fella", 
                "Home.png"
            );
            
            newWorld.LocationAt(-1, -1).AvailableQuests.Add(QuestFactory.GetQuestById(1));
            newWorld.LocationAt(-1, -1).Trader = TraderFactory.GetTraderById(1);

            newWorld.AddLocation(
                -2,
                -1,
                "Balcony",
                "Where are piles of wood, boxes and other mysterious stuff abandoned here, with mighty winds howling between them. And also evil pigeons are here too...",
                "Home.png"
            );
            
            newWorld.LocationAt(-2, -1).AddMonster(1, 10);
            newWorld.LocationAt(-2, -1).AddMonster(2, 1);
                
            newWorld.AddLocation(
                -1, 
                0, 
                "Meat Keeper",
                "Big grey fella, with so many medals on his chest. And his sides too. He also likes to trade",
                "Home.png"
            );
            
            newWorld.LocationAt(-1, 0).Trader = TraderFactory.GetTraderById(2);

 
            newWorld.AddLocation(
                0, 
                0, 
                "Central Square",
                "Nice place to run. Run fast. BEHOLD OF TYGYDYK!",
                "Home.png"
            );
 
            newWorld.AddLocation(
                1, 
                0, 
                "Portal to another dimension",
                "So many times I saw humans disappear behind it...",
                "Home.png"
            );
 
            newWorld.AddLocation(
                2, 
                0, 
                "Another Dimension",
                "...",
                "Home.png"
            );
            
            newWorld.LocationAt(2, 0).AddMonster(3, 10);
            newWorld.LocationAt(2, 0).AddMonster(2, 1);
            
            newWorld.AddLocation(
                0, 
                1, 
                "Chinchilla's hut",
                "Dat fella loves to trade some herbals for hairballs!",
                "Home.png"
            );
            
            newWorld.LocationAt(0, 1).Trader = TraderFactory.GetTraderById(3);

 
            newWorld.AddLocation(
                    0, 
                    2, 
                    "Dark room with WATER",
                    "Actually I like to drink water, but there are some evil spirits protecting it.",
                    "Home.png"
            );
            
            newWorld.LocationAt(0, 2).AddMonster(2, 10);

            return newWorld;
        }
    }
}