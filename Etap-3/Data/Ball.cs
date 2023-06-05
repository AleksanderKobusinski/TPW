using System;
using System.Numerics;
using System.Threading.Tasks;


namespace Data
{
    public class Ball
    {
        public int Id { get; }
        public Vector2 Position { get; private set; }
        public float R { get; }
        public float Mass { get; }
        public Vector2 Direction { get; set; }
        public bool IsAlive { get; set; }
        public event EventHandler<BallEventArgs> Moved;

        public Ball(int id, Vector2 position, float radius, float mass, Vector2 direction, bool isAlive)
        {
            Id = id;
            Position = position;
            R = radius;
            Mass = mass;
            Direction = direction;
            IsAlive = isAlive;
        }

        public async void MoveBall()
        {
            while (IsAlive)
            {
                Position += Direction;
                var args = new BallEventArgs(this);
                Moved?.Invoke(this, args);
                await Task.Delay(1);
            }
        }
    }
}
