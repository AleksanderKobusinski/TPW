using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class LogicTest
    {
        private static Vector2 _screenSize = new(400, 400);
        private static readonly string _logFileName = "testLogs" + DateTime.Now.ToString("HH_mm_ss_ffffff").ToString();
        private static readonly LogicApi _logicInstance = new(_screenSize, _logFileName);
        private static readonly Box _box = _logicInstance.GetBox();

        [TestInitialize]
        public void Init()
        {
            _logicInstance.CreateBalls(30, 4, 5, _box);
        }

        [TestMethod]
        public void StartStopMovementTest()
        {
            _logicInstance.StartSimulation(_box);
            foreach (Ball ball in _box.GetBalls())
            {
                Assert.AreEqual(true, ball.IsAlive);
            }

            _logicInstance.StopSimulation(_box);
            foreach (Ball ball in _box.GetBalls())
            {
                Assert.AreEqual(false, ball.IsAlive);
            }
        }
    }
}