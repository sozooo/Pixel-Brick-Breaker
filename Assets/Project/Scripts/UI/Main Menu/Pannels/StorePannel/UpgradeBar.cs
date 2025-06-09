using UI.Main_Menu.Pannels.StorePannel;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    [SerializeField] private UpgradeItem _upgradeItem;
    [SerializeField] private Image _fill;

    private readonly float _fillPerLevel = 0.33f;

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
        _fill.fillAmount = _fillPerLevel * level;
    }
}