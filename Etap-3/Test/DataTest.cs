using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class DataTest
    {
        private static Vector2 _screenSize = new(400, 400);
        private static readonly string _logFileName = "testLogs" + DateTime.Now.ToString("HH_mm_ss_ffffff").ToString();
        private static readonly LogicApi _logicInstance = new(_screenSize, _logFileName);
        private static readonly Box _box = _logicInstance.GetBox();
        private static readonly int _count = 30;
        private static readonly int _radius = 4;
        private static readonly int _mass = 5;

        [TestInitialize]
        public void Init()
        {
            _logicInstance.CreateBalls(_count, _radius, _mass, _box);
        }

        [TestMethod]
        public void BallTest()
        {
            foreach (Ball ball in _box.GetBalls())
            {
                Assert.AreEqual(_radius, ball.R);
                Assert.IsTrue(ball.Position.X >= 0 && ball.Position.X <= _box.ScreenSize.X - ball.R);
                Assert.IsTrue(ball.Position.Y >= 0 && ball.Position.Y <= _box.ScreenSize.Y - ball.R);
            }
        }

        [TestMethod]
        public void BoxTest()
        {
            Assert.AreEqual(_screenSize.X, _box.ScreenSize.X);
            Assert.AreEqual(_screenSize.Y, _box.ScreenSize.X);
            Assert.AreEqual(_count, _box.GetBalls().Count);
        }

        [TestMethod]
        public void CollisionTest()
        {
            Movement movement = new Movement(new Random(), _screenSize, _box.GetBalls());
            Vector2 pos1 = new(80, 80);
            float radius1 = 15;
            Vector2 pos2 = new(80, 80);
            float radius2 = 15;

            Assert.IsTrue(movement.DoBallsCollide(pos1, radius1, pos2, radius2));
        }

        [TestMethod]
        public void LogFileCreationTest()
        {
            Assert.IsTrue(File.Exists(BallsMovementLogs.Path + _logFileName + ".json"));
        }
    }
}
