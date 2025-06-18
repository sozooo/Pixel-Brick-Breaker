using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Figure
{
    [Serializable]
    public class FigureColliderBuilder
    {
        [SerializeField] private GameObject _colliderHolder;
        
        private FigureConfig _config;
        private IVoxelChecker _voxelChecker;

        private readonly List<BoxCollider> _colliders = new();
        
        public void Initialize(FigureConfig config, IVoxelChecker meshBuilder)
        {
            _config = config;
            _voxelChecker = meshBuilder;
        }
        
        public void RebuildColliders()
        {
            DisableColliders();
            
            BuildColliders();
        }

        public void DisableColliders()
        {
            foreach (BoxCollider collider in _colliders)
                collider.enabled = false;
        }
        
        private bool TryGetCollider(out BoxCollider boxCollider)
        {
            boxCollider = _colliders.FirstOrDefault(collider => collider.enabled == false);
            
            return boxCollider != null;
        }
        
        private void BuildColliders()
        {
            var used = new bool[_config.Width, _config.Height];

            for (int y = 0; y < _config.Height; y++)
            {
                for (int x = 0; x < _config.Width; x++)
                {
                    if (used[x, y] || _voxelChecker.VoxelExists(x, y) == false) 
                        continue;
                    
                    int width = 1;
                    int height = 1;

                    while (x + width < _config.Width && _voxelChecker.VoxelExists(x + width, y) && used[x + width, y] == false)
                        width++;

                    bool canGrow = true;
                    
                    while (canGrow && y + height < _config.Height)
                    {
                        for (int dx = 0; dx < width; dx++)
                        {
                            if (_voxelChecker.VoxelExists(x + dx, y + height) && used[x + dx, y + height] == false) 
                                continue;
                            
                            canGrow = false;
                            break;
                        }
                        if (canGrow) height++;
                    }

                    for (int deltaY = 0; deltaY < height; deltaY++)
                    {
                        for (int deltaX = 0; deltaX < width; deltaX++)
                        {
                            used[x + deltaX, y + deltaY] = true;
                        }
                    }
                    
                    if(TryGetCollider(out BoxCollider boxCollider) == false)
                        boxCollider = _colliderHolder.AddComponent<BoxCollider>();

                    boxCollider.enabled = true;
                    boxCollider.center = new Vector3(x + width / 2f, y + height / 2f, 0.5f);
                    boxCollider.size = new Vector3(width, height, 1);
                    
                    _colliders.Add(boxCollider);
                }
            }
        }
    }
}