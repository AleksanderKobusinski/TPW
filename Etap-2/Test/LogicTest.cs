using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void StartStopMovementTest()
        {
            Vector2 screenSize = new Vector2(400, 400);
            LogicApi LogicApi = new LogicApi(screenSize);
            Box box = LogicApi.GetBox();
            LogicApi.CreateBalls(8, 5, 4, box);

            LogicApi.StartSimulation(box);
            foreach (Ball ball in box.GetBalls())
            {
                Assert.AreEqual(true, ball.IsAlive);
            }

            LogicApi.StopSimulation(box);
            foreach (Ball ball in box.GetBalls())
            {
                Assert.AreEqual(false, ball.IsAlive);
            }
        }
    }
}