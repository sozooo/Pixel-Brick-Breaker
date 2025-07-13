using Project.Scripts.UI.Level.ExtraTime;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Game;
using TMPro;
using UniRx;
using UnityEngine;

namespace Project.Scripts.UI.Level.EndGamePannel.EndGameRows
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class EndGameRow : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private ExtraTimeManager _extraTimeManager;
    
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