using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UnityEngine;
using WorkObjects.Enums;

namespace Project.Scripts.Figure
{
    [Serializable]
    public class FigureMeshBuilder : IVoxelChecker
    {
        [SerializeField] private MeshFilter _meshFilter;

        private readonly HashSet<Vector2Int> _removedVoxels = new HashSet<Vector2Int>();
        private Voxel[,] _voxels;
    
        private FigureConfig _config;
        private Transform _transform;
        
        public event Action OnVoxelFell;
        
        public void Initialize(FigureConfig config, Transform transform)
        {
            _config = config;
            _transform = transform;
            
            _removedVoxels.Clear();
            
            _voxels = new Voxel[_config.width, _config.height];
            
            for (int x = 0; x < _config.width; x++) 
            {
                for (int y = 0; y < _config.height; y++)
                {
                    Voxel voxel = _config.Voxels.FirstOrDefault(voxel => voxel.Position == new Vector2Int(x, y));

                    if (voxel != null)
                    {
                        _voxels[x, y] = voxel;
                        
                        continue;
                    }
                    
                    _removedVoxels.Add(new Vector2Int(x, y));
                }
            }
        }
        
        public void ApplyDamage(Vector2 contactPosition, float radius) 
        {
            for (int x = 0; x < _config.width; x++) 
            {
                for (int y = 0; y < _config.height; y++) 
                {
                    Vector2Int position = new Vector2Int(x, y);
                    
                    if (_removedVoxels.Contains(position)) 
                        continue;

                    float distance = Vector2.Distance(contactPosition, position);

                    if (distance > radius) 
                        continue;
                    
                    DetatchVoxel(position);
                }
            }

            Rebuild();
        }
        
        public bool VoxelExists(int x, int y) 
        {
            if (x < 0 || x >= _config.width || y < 0 || y >= _config.height) 
                return false;
            
            return _removedVoxels.Contains(new Vector2Int(x, y)) == false;
        }

        public void VoxelsFall()
        {
            for (int x = 0; x < _config.width; x++) 
            {
                for (int y = 0; y < _config.height; y++) 
                {
                    Vector2Int position = new Vector2Int(x, y);
                    
                    if (_removedVoxels.Contains(position)) 
                        continue;

                    DetatchVoxel(position);
                }
            }
            
            Rebuild();
        }
        
        #region Rebuild Mesh
        
        public void Rebuild() 
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Color> colors = new List<Color>();

            int quadIndex = 0;

            for (int x = 0; x < _config.width; x++) 
            {
                for (int y = 0; y < _config.height; y++) 
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    
                    if (_removedVoxels.Contains(pos)) 
                        continue;

                    Voxel voxel = _voxels[x, y];
                    Color color = voxel.Color;
                    
                    AddSideQuad(x, y, Directions.Front, color, vertices, triangles, colors, ref quadIndex);

                    if (VoxelExists(x - 1, y) == false) 
                        AddSideQuad(x, y, Directions.Left, color, vertices, triangles, colors, ref quadIndex);

                    if (VoxelExists(x + 1, y) == false) 
                        AddSideQuad(x, y, Directions.Right, color, vertices, triangles, colors, ref quadIndex);

                    if (VoxelExists(x, y - 1) == false) 
                        AddSideQuad(x, y, Directions.Down, color, vertices, triangles, colors, ref quadIndex);

                    if (VoxelExists(x, y + 1) == false) 
                        AddSideQuad(x, y, Directions.Up, color, vertices, triangles, colors, ref quadIndex);
                }
            }

            Mesh mesh = new Mesh
            {
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                colors = colors.ToArray()
            };
            
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            _meshFilter.mesh = mesh;
        }

        private void AddQuadTriangles(List<int> triangles, int start) 
        {
            triangles.Add(start + 0);
            triangles.Add(start + 2);
            triangles.Add(start + 1);

            triangles.Add(start + 1);
            triangles.Add(start + 2);
            triangles.Add(start + 3);
        }

        private void AddVertexColors(List<Color> colors, Color color) 
        {
            for (int i = 0; i < 4; i++) 
                colors.Add(color);
        }
        
        private void AddSideQuad(int x, int y, Directions direction, Color color,
            List<Vector3> vertices, List<int> triangles, List<Color> colors, ref int quadIndex)
        {
            Vector3 depthOffset = Vector3.forward;

            Vector3 baseBottomLeft = new Vector3(x, y, 0);
            Vector3 baseBottomRight = new Vector3(x + 1, y, 0);
            Vector3 baseTopLeft = new Vector3(x, y + 1, 0);
            Vector3 baseTopRight = new Vector3(x + 1, y + 1, 0);

            Vector3 bottomLeft, bottomRight, topLeft, topRight;

            switch (direction)
            {
                case Directions.Left:
                    bottomLeft = baseBottomLeft;
                    bottomRight = baseBottomLeft + depthOffset;
                    topLeft = baseTopLeft;
                    topRight = baseTopLeft + depthOffset;
                    vertices.AddRange(new[] { bottomLeft, topLeft, bottomRight, topRight });
                    break;
                
                case Directions.Right:
                    bottomLeft = baseBottomRight;
                    bottomRight = baseBottomRight + depthOffset;
                    topLeft = baseTopRight;
                    topRight = baseTopRight + depthOffset;
                    vertices.AddRange(new[] { bottomLeft, bottomRight, topLeft, topRight });
                    break;
                
                case Directions.Down:
                    bottomLeft = baseBottomLeft;
                    bottomRight = baseBottomLeft + depthOffset;
                    topLeft = baseBottomRight;
                    topRight = baseBottomRight + depthOffset;
                    vertices.AddRange(new[] { bottomLeft, bottomRight, topLeft, topRight });
                    break;
                
                case Directions.Up:
                    bottomLeft = baseTopLeft;
                    bottomRight = baseTopLeft + depthOffset;
                    topLeft = baseTopRight;
                    topRight = baseTopRight + depthOffset;
                    vertices.AddRange(new[] { bottomLeft, topLeft, bottomRight, topRight });
                    break;

                case Directions.Front:
                default:
                    vertices.AddRange(new[] { baseBottomLeft, baseBottomRight, baseTopLeft, baseTopRight });
                    break;
            }

            AddQuadTriangles(triangles, quadIndex);
            AddVertexColors(colors, color);
            quadIndex += 4;
        }
        
        #endregion
        
        private void DetatchVoxel(Vector2Int position)
        {
            _removedVoxels.Add(position);
                        
            MessageBrokerHolder.Figure.Publish(new M_VoxelFell(_transform.TransformPoint((Vector3Int)position), 
                _transform.rotation, _transform.localScale, _config.Voxels.First(voxel => voxel.Position == position).Color));
            
            OnVoxelFell?.Invoke();
        }
    }
}