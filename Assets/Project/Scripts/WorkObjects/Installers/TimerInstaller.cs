using Project.Scripts.UI.Timer;
using Project.Scripts.WorkObjects.Handlers;
using Zenject;

namespace Project.Scripts.WorkObjects.Installers
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