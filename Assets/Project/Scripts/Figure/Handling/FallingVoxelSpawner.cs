using System;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;

namespace Project.Scripts.Figure.Handling
{
    [Serializable]
    public class FallingVoxelSpawner : Spawner<FallingVoxel>
    {
        public override FallingVoxel Spawn()
        {
            FallingVoxel voxel = Pool.Give();
            
            voxel.Despawned += OnDespawned;
            voxel.gameObject.SetActive(true);
            
            return voxel;
        }

        public void InitializeVoxel(M_VoxelFell message)
        {
            FallingVoxel voxel = Spawn();
            
            voxel.Initialize(message.Position, message.Rotation);
            voxel.ApplyColor(message.Color);
        }

        protected override void OnDespawned(FallingVoxel spawnable)
        {
            base.OnDespawned(spawnable);
            
            Pool.Add(spawnable);
        }
    }
}