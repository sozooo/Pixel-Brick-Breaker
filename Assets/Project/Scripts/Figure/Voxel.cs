using System;
using UnityEngine;

[Serializable]
public class Voxel
{
    public Voxel(Vector2Int pos, Color col) {
        Position = pos;
        Color = col;
    }
    
    [field: SerializeField] public Vector2Int Position { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}