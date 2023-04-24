using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class Ball
    {
        public double X;
        public double Y;
        public int R;

        private double _speedX;
        private double _speedY;
        private bool _isAlive;

        public event PropertyChangedEventHandler PropertyChanged;

        internal Ball(double x, double y, int radius)
        {
            this.X = x;
            this.Y = y;
            this.R = radius;

            this._speedX = 4.0;
            this._speedY = 4.0;

            this._isAlive = true;
        }

        internal void MoveBall()
        {
            double tmp_x, tmp_y;

            tmp_x = PosX + XSpeed;
            tmp_y = PosY + YSpeed;

            this.CheckX(tmp_x);
            this.CheckY(tmp_y);

            RaisePropertyChanged(nameof(PosX));
            RaisePropertyChanged(nameof(PosY));

        }

        public void CheckX(double tmp_x)
        {
            if (tmp_x >= 400 - Radius * 2)
            {
                this.PosX = 400 - Radius * 2;
                XSpeed *= -1;
            }
            else if (tmp_x <= 0)
            {
                this.PosX = 0;
                XSpeed *= -1;
            }
            else
            {
                this.PosX = PosX + XSpeed;
            }
        }

        public void CheckY(double tmp_y)
        {
            if (tmp_y + Radius * 2 >= 400)
            {
                this.PosY = 400 - Radius * 2;
                YSpeed *= -1;
            }
            else if (tmp_y <= 0)
            {
                this.PosY = 0;
                YSpeed *= -1;
            }
            else
            {
                this.PosY = PosY + YSpeed;
            }
        }

        public double PosX
        {
            get => X;
            set => X = value;
        }

        public double PosY
        {
            get => Y;
            set => Y = value;
        }

        public double XSpeed
        {
            get => _speedX;
            set => _speedX = value;
        }

        public double YSpeed
        {
            get => _speedY;
            set => _speedY = value;
        }

        public int Radius
        {
            get => R;
            set
            {
                if (value > 0) R = value;
                else throw new ArgumentException("Wrong R value");
            }
        }

        public bool Alive { get => _isAlive; set => _isAlive = value; }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
