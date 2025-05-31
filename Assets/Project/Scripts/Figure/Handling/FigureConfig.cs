using System.Collections.Generic;
using Project.Scripts.Figure;
using UnityEngine;

[CreateAssetMenu(fileName = "FigureConfig", menuName = "Figures/New Figure Config", order = 51)]
public class FigureConfig : ScriptableObject
{
    [SerializeField] public List<Voxel> Voxels;
    [SerializeField, Range(0, 24)] public int width = 16;
    [SerializeField, Range(0, 24)] public int height = 16;
}