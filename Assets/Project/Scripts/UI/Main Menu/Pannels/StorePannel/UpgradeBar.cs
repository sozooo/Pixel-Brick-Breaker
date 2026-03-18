using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    public class UpgradeBar : MonoBehaviour
    {
        private const int MaxLevel = 3;

        [SerializeField] private UpgradeItem _upgradeItem;
        [SerializeField] private Slider _slider;
        
        private Tween _tween;

        private void Awake()
        {
            _slider.minValue = 0;
            _slider.maxValue = MaxLevel;
            _slider.wholeNumbers = true;
        }

        private void OnEnable()
        {
            _upgradeItem.Upgraded += Fill;
        }

        private void OnDisable()
        {
            _upgradeItem.Upgraded -= Fill;
        }

        private void Fill(int level)
        {
            float target = level;

            _tween?.Kill();
            _tween = DOTween.To(() => _slider.value, x => _slider.value = x, target, 0.25f);
        }
    }
}