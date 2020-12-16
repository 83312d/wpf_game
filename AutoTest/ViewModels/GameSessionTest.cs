using Core.ViewModels;
using NUnit.Framework;

namespace AutoTest.ViewModels
{
    [TestFixture]
    public class GameSessionTest
    {
        [Test]
        public void CreateGameSessionTest()
        {
            GameSession gameSession = new GameSession();
            
            Assert.IsNotNull(gameSession.CurrentPlayer);
            Assert.AreEqual("Central Square", gameSession.CurrentLocation.Name);
        }

        [Test]
        public void PlayerRespawnAtHomeAndHealOnDefeat()
        {
            GameSession gameSession = new GameSession();
            
            gameSession.CurrentPlayer.TakeDamage(99999);
            Assert.AreEqual("Home", gameSession.CurrentLocation.Name);
            Assert.AreEqual(gameSession.CurrentPlayer.MaxHitPoints, gameSession.CurrentPlayer.CurrentHitPoints);
        }
    }
}