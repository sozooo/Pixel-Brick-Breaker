using UnityEngine;

namespace Project.Scripts.WorkObjects.MessageBrokers.Figure
{
    public struct M_VoxelFell
    {
        public M_VoxelFell(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Color = color;
        }
        
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Color Color { get; private set; }
        public Vector3 Scale { get; private set; }
    }
}