using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class DataTest
    {
        private static Vector2 screenSize = new(400, 400);
        static readonly LogicApi LogicInstance = new(screenSize);
        static readonly Box box = LogicInstance.GetBox();
        static readonly int count = 30;
        static readonly int radius = 4;
        static readonly int mass = 5;

        [TestInitialize]
        public void Init()
        {
            LogicInstance.CreateBalls(count, radius, mass, box);
        }

        [TestMethod]
        public void BallTest()
        {
            foreach (Ball ball in box.GetBalls())
            {
                Assert.AreEqual(radius, ball.R);
                Assert.IsTrue(ball.Position.X >= 0 && ball.Position.X <= box.ScreenSize.X - ball.R);
                Assert.IsTrue(ball.Position.Y >= 0 && ball.Position.Y <= box.ScreenSize.Y - ball.R);
            }
        }

        [TestMethod]
        public void BoxTest()
        {
            Assert.AreEqual(screenSize.X, box.ScreenSize.X);
            Assert.AreEqual(screenSize.Y, box.ScreenSize.X);
            Assert.AreEqual(count, box.GetBalls().Count);
        }

        [TestMethod]
        public void CollisionTest()
        {
            Movement movement = new Movement(new Random(), screenSize, box.GetBalls());
            Vector2 pos1 = new(80, 80);
            float radius1 = 15;
            Vector2 pos2 = new(80, 80);
            float radius2 = 15;

            Assert.IsTrue(movement.DoBallsCollide(pos1, radius1, pos2, radius2));
        }
    }
}
