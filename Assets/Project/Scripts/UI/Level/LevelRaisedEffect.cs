using Project.Scripts.WorkObjects.Handlers;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Game;
using UniRx;
using UnityEngine;

namespace Project.Scripts.UI.Level
{
    public class LevelRaisedEffect : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Audio _audio;
        
        private void OnEnable()
        {
            MessageBrokerHolder.Game
                .Receive<M_LevelRaised>()
                .Subscribe(_ => HandleLevelRaised())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable?.Clear();
        }

        private void HandleLevelRaised()
        {
            _particle.Play();
            _audio.PlayOneShot();
        }
    }
}