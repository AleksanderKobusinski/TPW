using System;
using System.Numerics;

namespace Model
{
    public class MovementEventArgs : EventArgs
    {
        public int Id;
        public Vector2 Position;
        public float Radius;
        public MovementEventArgs(int id, Vector2 position, float radius)
        {
            Id = id;
            Position = position;
            Radius = radius;
        }
    }
}
