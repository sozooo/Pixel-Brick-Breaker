using UnityEngine;
using UnityEngine.UI;

namespace UI.Level
{
    public class ExitGameplayButton : MonoBehaviour
    {
        [SerializeField] private ExtraTimeManager _extraTimeManager;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleExitGameplay);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleExitGameplay);
        }

        private void HandleExitGameplay()
        {
            _extraTimeManager.EndGame();
        }
    }
}