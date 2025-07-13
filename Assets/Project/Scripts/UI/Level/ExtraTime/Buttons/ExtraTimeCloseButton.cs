using Project.Scripts.UI.Main_Menu.Pannels;
using UnityEngine;

namespace Project.Scripts.UI.Level.ExtraTime.Buttons
{
    public class ExtraTimeCloseButton : CloseButton
    {
        [SerializeField] private ExtraTimeManager _extraTimeManager;

        protected override void Close()
        {
            _extraTimeManager.EndGame();
            base.Close();
        }
    }
}