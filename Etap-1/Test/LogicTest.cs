using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void StartStopMovementTest()
        {

            LogicApi LogicApi = new LogicApi();
            Box box = LogicApi.CreateBox();
            LogicApi.CreateBalls(3, 5, box);


            LogicApi.StartSimulation(box);
            foreach (Ball ball in box.Balls)
            {
                Assert.AreEqual(true, ball.Alive);
            }

            LogicApi.StopSimulation(box);
            foreach (Ball ball in box.Balls)
            {
                Assert.AreEqual(false, ball.Alive);
            }
        }
    }
}