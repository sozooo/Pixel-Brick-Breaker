using TMPro;
using UnityEngine;

namespace UI.Level.EndGamePannel.EndGameRows
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class EndGameRow : MonoBehaviour
    {
        [SerializeField] private ExtraTimeManager _extraTimeManager;
    
        protected TextMeshProUGUI Text;

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _extraTimeManager.GameOvered += Display;
        }

        private void OnDisable()
        {
            _extraTimeManager.GameOvered -= Display;
        }

        protected abstract void Display();
    }
}