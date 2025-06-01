using WorkObjects.Handlers;
using Zenject;

namespace Project.Scripts.WorkObjects
{
    public class TimerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TimerProgressBar>().FromComponentInHierarchy().AsSingle();

            Container.Bind<TimerHandler>().FromNew().AsSingle();
        }
    }
}