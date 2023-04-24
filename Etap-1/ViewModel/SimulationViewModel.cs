using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class SimulationViewModel
    {

        private readonly Box _box;
        private readonly BallsMovement _logicInstance;
        private int _count;

        public SimulationViewModel()
        {
            _logicInstance = new BallsMovement();
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
            _logicInstance.CreateBalls(Count, Radius, _box);
            _logicInstance.StartSimulation(_box);
        }

        public void RunStopSimulation()
        {
            _logicInstance.StopSimulation(_box);
        }
    }
}

