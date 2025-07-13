using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;

namespace Project.Scripts.UI.Coins
{
    public class BonusRewardGroup : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private CanvasRenderer _bonus;

        private void OnEnable()
        {
            MessageBrokerHolder.Figure
                .Receive<M_FigureFell>()
                .Subscribe(_ => ShowReward())
                .AddTo(_disposable);
            
            MessageBrokerHolder.Figure
                .Receive<M_FigureDespawned>()
                .Subscribe(_ => HideReward())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable?.Clear();
        }

        private void ShowReward()
        {
            _bonus.gameObject.SetActive(true);
        }

        private void HideReward()
        {
            _bonus.gameObject.SetActive(false);
        }
    }
}