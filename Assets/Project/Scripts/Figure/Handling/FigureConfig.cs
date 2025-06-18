using System.Collections.Generic;
using Project.Scripts.Figure;
using UnityEngine;

[CreateAssetMenu(fileName = "FigureConfig", menuName = "Figures/New Figure Config", order = 51)]
public class FigureConfig : ScriptableObject
{
    [field: SerializeField] public List<Voxel> Voxels { get; private set; }
    [field: SerializeField, Range(0, 24)] public int Width { get; private set; } = 16;
    [field: SerializeField, Range(0, 24)] public int Height { get; private set; } = 16;
    [field: SerializeField] public Vector3 Scale { get; private set; }
}