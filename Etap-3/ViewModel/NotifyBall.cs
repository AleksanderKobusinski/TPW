using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class NotifyBall : INotifyPropertyChanged
    {
        private double _X;
        private double _Y;
        private double _R;

        public NotifyBall()
        {
            var position = new Vector2(0, 0);
            PosX = position.X;
            PosY = position.Y;
            Radius = 0;
        }

        public double PosX
        {
            get { return _X; }
            set { _X = value; OnPropertyChanged(); }
        }

        public double PosY
        {
            get { return _Y; }
            set { _Y = value; OnPropertyChanged(); }
        }

        public double Radius
        {
            get { return _R; }
            set { _R = value; OnPropertyChanged(); }
        }

        public void ChangePosition(Vector2 position)
        {
            PosX = position.X;
            PosY = position.Y;
        }

        public void ChangeRadius(float R)
        {
            Radius = R;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
