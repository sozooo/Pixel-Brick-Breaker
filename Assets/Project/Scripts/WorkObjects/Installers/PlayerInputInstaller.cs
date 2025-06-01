using WorkObjects.Handlers;
using Zenject;

namespace Project.Scripts.WorkObjects
{
    public class PlayerInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInput>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<GamePauser>().FromNew().AsSingle().NonLazy();
        }
    }
}