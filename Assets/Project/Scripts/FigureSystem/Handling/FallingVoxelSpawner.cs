using System;
using Project.Scripts.WorkObjects;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;

namespace Project.Scripts.FigureSystem.Handling
{
    [Serializable]
    public class FallingVoxelSpawner : Spawner<FallingVoxel>
    {
        public void InitializeVoxel(M_VoxelFell message)
        {
            FallingVoxel voxel = Spawn();
            
            voxel.gameObject.SetActive(true);
            
            voxel.Initialize(message.Position, message.Rotation);
            voxel.ApplyColor(message.Color);
            voxel.ApplySacle(message.Scale);
        }

        protected override void OnDespawned(FallingVoxel fallingVoxel)
        {
            base.OnDespawned(fallingVoxel);
            
            Pool.Add(fallingVoxel);
        }
    }
}