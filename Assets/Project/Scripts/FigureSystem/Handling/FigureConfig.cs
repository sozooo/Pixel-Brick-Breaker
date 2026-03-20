using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling
{
    [CreateAssetMenu(fileName = "FigureConfig", menuName = "Figures/New Figure Config", order = 51)]
    public class FigureConfig : ScriptableObject
    {
        [field: SerializeField] public List<Voxel> Voxels { get; private set; }
        [field: SerializeField] [field: Range(0, 25)] public int Width { get; private set; } = 16;
        [field: SerializeField][field: Range(0, 24)] public int Height { get; private set; } = 16;
        [field: SerializeField] public Vector3 Scale { get; private set; }

        public void Initialize(List<Voxel> voxels, int width, int height, Vector3 scale)
        {
            Voxels = voxels;
            Width = width;
            Height = height;
            Scale = scale;
        }

        public void FlipY()
        {
            if (Voxels == null || Voxels.Count == 0) 
                return;

            int minY = Voxels.Min(v => v.Position.y);
            int maxY = Voxels.Max(v => v.Position.y);

            foreach (var voxel in Voxels)
            {
                var pos = voxel.Position;
                int newY = minY + maxY - pos.y;
                voxel.SetPosition(new Vector2Int(pos.x, newY));
            }
        }

        public void FlipX()
        {
            if (Voxels == null || Voxels.Count == 0) 
                return;

            int minX = Voxels.Min(v => v.Position.x);
            int maxX = Voxels.Max(v => v.Position.x);

            foreach (var voxel in Voxels)
            {
                var pos = voxel.Position;
                int newX = minX + maxX - pos.x;
                voxel.SetPosition(new Vector2Int(newX, pos.y));
            }
        }
    }
}