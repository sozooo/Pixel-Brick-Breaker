using Project.Scripts.WorkObjects.MessageBrokers;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Level.EndGamePannel.EndGameRows
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class EndGameRow : MonoBehaviour
    {
        [SerializeField] private ExtraTimeManager _extraTimeManager;
    
        private readonly CompositeDisposable _disposable = new();
        protected TextMeshProUGUI Text;

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            MessageBrokerHolder.Game
                .Receive<M_GameOvered>()
                .Subscribe(_ => Display())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }

        protected abstract void Display();
    }
}