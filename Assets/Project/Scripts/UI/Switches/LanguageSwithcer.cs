using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;

namespace Project.Scripts.UI.Switches
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

            _dropdown.value = _dropdown.options.IndexOf(_dropdown.options
                .FirstOrDefault(option => string
                    .Equals(option.text, YG2.lang, StringComparison.CurrentCultureIgnoreCase)));
        }
        
        private void OnDisable()
        {
            _dropdown.onValueChanged.RemoveListener(SwitchLanguage);
        }

        private void SwitchLanguage(int index)
        {
            YG2.SwitchLanguage(_languageNames[index]);
        }
    }
}