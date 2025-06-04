using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using Zenject;

namespace Project.Scripts.WorkObjects
{
    public class PlayerStatsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats playerStats;
        
        public override void InstallBindings()
        {
            Container.Bind<PurchaseItem>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<RemoveAdItem>().FromComponentInHierarchy().AsSingle();

            Container.Bind<PlayerStats>().FromInstance(playerStats).AsSingle();
        }
    }
}