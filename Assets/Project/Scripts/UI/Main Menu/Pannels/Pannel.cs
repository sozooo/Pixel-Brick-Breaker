using UnityEngine;
using YG;

namespace Project.Scripts.UI.Main_Menu.Pannels
{
    public class Pannel : MonoBehaviour
    {
        protected void OnEnable()
        {
            Display();
        }

        protected virtual void Display()
        {
            if (YG2.saves.IsAdRemoved == false)
                YG2.StickyAdActivity(true);
        }
    }
}