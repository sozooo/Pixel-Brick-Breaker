using System.Collections.Generic;
using Project.Scripts.UI.Main_Menu.Pannels.StorePannel;
using Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Project.Scripts.WorkObjects.Installers
{
    public class PlayerStatsInstaller : MonoInstaller
    {
        [SerializeField] private List<PurchaseItem> _purchases;
        [SerializeField] private RemoveAdItem _removeAd;
        
        public override void InstallBindings()
        {
            Container.Bind<List<PurchaseItem>>().FromInstance(_purchases).AsSingle();
            Container.Bind<RemoveAdItem>().FromInstance(_removeAd).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStats>().FromNew().AsSingle();
        }
    }
}