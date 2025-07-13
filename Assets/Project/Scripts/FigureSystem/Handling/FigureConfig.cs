using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling
{
    [CreateAssetMenu(fileName = "FigureConfig", menuName = "Figures/New Figure Config", order = 51)]
    public class FigureConfig : ScriptableObject
    {
        [field: SerializeField] public List<Voxel> Voxels { get; private set; }
        [field: SerializeField] [field: Range(0, 24)] public int Width { get; private set; } = 16;
        [field: SerializeField][field: Range(0, 24)] public int Height { get; private set; } = 16;
        [field: SerializeField] public Vector3 Scale { get; private set; }
    }
}