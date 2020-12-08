using System;

namespace Core
{
    public static class GodOfRandom
    {
        private static readonly Random Generator = new Random();
        
        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            return Generator.Next(minimumValue, maximumValue + 1);
        }
    }
}