using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Main_Menu.Pannels.StorePannel
{
    [RequireComponent(typeof(Button))]
    public class MaxLevelHandler : MonoBehaviour
    {
        [SerializeField] private UpgradeItem _upgradeItem;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private TextMeshProUGUI _maxLevelText;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _upgradeItem.LevelMaxed += Handle;
        }

        private void OnDisable()
        {
            _upgradeItem.LevelMaxed -= Handle;
        }

        private void Handle()
        {
            _buttonText.gameObject.SetActive(false);
            _maxLevelText.gameObject.SetActive(true);
            
            _button.interactable = false;
        }
    }
}