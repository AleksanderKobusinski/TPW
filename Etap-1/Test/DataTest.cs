using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class DataTest
    {
        LogicApi LogicInstance = new LogicApi();

        [TestMethod]
        public void BallTest()
        {
            Box box = LogicInstance.CreateBox();
            LogicInstance.CreateBalls(30, 4, box);

            foreach (Ball ball in box.Balls)
            {
                Assert.AreEqual(4, ball.R);
                Assert.IsTrue(ball.PosX >= 0 && ball.PosX <= box.Height - ball.Radius);
                Assert.IsTrue(ball.PosY >= 0 && ball.PosY <= box.Width - ball.Radius);
            }
        }

        [TestMethod]
        public void BoxTest()
        {
            Box box = LogicInstance.CreateBox();
            LogicInstance.CreateBalls(10, 5, box);

            Assert.AreEqual(box.Height, 400);
            Assert.AreEqual(box.Width, 400);
            Assert.AreEqual(box.Balls.Count, 10);
        }
    }
}
