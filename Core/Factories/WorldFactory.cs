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
                "/Core;component/Art/Locations/Home.png"
            );
            
            newWorld.AddLocation(
                -1, 
                -1, 
                "Machines Domain", 
                "Strange place populated by Computers, Consoles and TV Fella", 
                "/Core;component/Art/Locations/Home.png"
            );

            newWorld.AddLocation(
                -2,
                -1,
                "Balcony",
                "Where are piles of wood, boxes and other mysterious stuff abandoned here, with mighty winds howling between them. And also evil pigeons are here too...",
                "/Core;component/Art/Locations/Home.png"
            );
                
            newWorld.AddLocation(
                -1, 
                0, 
                "Meat Keeper",
                "Big grey fella, with so many medals on his chest. And his sides too. He also likes to trade",
                "/Core;component/Art/Locations/Home.png"
            );
 
            newWorld.AddLocation(
                0, 
                0, 
                "Central Square",
                "Nice place to run. Run fast. BEHOLD OF TYGYDYK!",
                "/Core;component/Art/Locations/Home.png"
            );
 
            newWorld.AddLocation(
                1, 
                0, 
                "Portal to another dimension",
                "So many times I saw humans disappear behind it...",
                "/Core;component/Art/Locations/Home.png"
            );
 
            newWorld.AddLocation(
                2, 
                0, 
                "Another Dimension",
                "...",
                "/Core;component/Art/Locations/Home.png"
            );
 
            newWorld.AddLocation(
                0, 
                1, 
                "Chinchilla's hut",
                "Dat fella loves to trade some herbals for hairballs!",
                "/Core;component/Art/Locations/Home.png"
            );
 
            newWorld.AddLocation(
                    0, 
                    2, 
                    "Dark room with WATER",
                    "Actually I like to drink water. But there are some evil spirits protecting it.",
                    "/Core;component/Art/Locations/Home.png"
            );
            
            return newWorld;
        }
    }
}