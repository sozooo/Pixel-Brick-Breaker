using System;
using UnityEngine;
using YG;

namespace UI.Main_Menu.Pannels.StorePannel.BlastRadius
{
    public class BlastRadiusItem : UpgradeItem
    {
        private void Awake()
        {
            CurrentLevel = YandexGame.savesData.BlastRadiusLevel;
        }

        protected override void Buy()
        {
            base.Buy();
            
            YandexGame.savesData.BlastRadiusLevel = CurrentLevel;
        }
    }
}