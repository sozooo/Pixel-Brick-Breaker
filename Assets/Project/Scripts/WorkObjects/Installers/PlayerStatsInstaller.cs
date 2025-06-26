using System.Collections.Generic;
using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using Zenject;

namespace Project.Scripts.WorkObjects
{
    public class PlayerStatsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private List<PurchaseItem> _purchases;
        [SerializeField] private RemoveAdItem _removeAd;
        
        public override void InstallBindings()
        {
            Container.Bind<List<PurchaseItem>>().FromInstance(_purchases).AsSingle();
            Container.Bind<RemoveAdItem>().FromInstance(_removeAd).AsSingle();

            Container.Bind<PlayerStats>().FromInstance(playerStats).AsSingle();
        }
    }
}