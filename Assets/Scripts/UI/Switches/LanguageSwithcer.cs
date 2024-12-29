using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;

namespace UI.Switches
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class LanguageSwithcer : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;
        private List<string> _languageNames;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            
            _languageNames = _dropdown.options.Select(option => option.text.ToLower()).ToList();
        }

        private void OnEnable()
        {
            _dropdown.onValueChanged.AddListener(SwitchLanguage);
        }

        private void SwitchLanguage(int index)
        {
            YandexGame.SwitchLanguage(_languageNames[index]);
        }
    }
}