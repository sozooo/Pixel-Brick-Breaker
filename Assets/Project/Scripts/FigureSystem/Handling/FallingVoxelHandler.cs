using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;

namespace Project.Scripts.FigureSystem.Handling
{
    public class FallingVoxelHandler : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private FallingVoxelSpawner _fallingVoxelSpawner;
        
        private void OnEnable()
        {
            MessageBrokerHolder.Figure
                .Receive<M_VoxelFell>()
                .Subscribe(OnVoxelFell)
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }

        private void OnVoxelFell(M_VoxelFell voxelFell)
        {
            _fallingVoxelSpawner.InitializeVoxel(voxelFell);
        }
    }
}