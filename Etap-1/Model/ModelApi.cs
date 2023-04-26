using Data;
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ModelApi
    {
        private Box _box;
        private int _count;
        public LogicApi LogicInstance;

        public ModelApi()
        {
            LogicInstance = new LogicApi();
            _box = new Box();
        }

        public static int Radius => 7;

        public int Count { get => _count; set => _count = value; }

        public ObservableCollection<Ball> Balls
        {
            get
            {
                return _box.Balls;
            }
        }

        public void RunStartSimulation()
        {
            LogicInstance.CreateBalls(Count, Radius, _box);
            LogicInstance.StartSimulation(_box);
        }

        public void RunStopSimulation()
        {
            LogicInstance.StopSimulation(_box);
        }
    }
}
