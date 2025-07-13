using Project.Scripts.UI.Level.ExtraTime;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Level
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