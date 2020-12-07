using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ViewModels
{
    public class GameSession
    {
        private Player CurrentPlayer { get; set; }

        public GameSession()
        {
            CurrentPlayer = new Player();

            CurrentPlayer.Name = "Shafto";
            CurrentPlayer.CharacterClass = "Pirate";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.Gold = 0;
            CurrentPlayer.Level = 1;
            CurrentPlayer.ExperiencePoints = 0;
        }
    }
}