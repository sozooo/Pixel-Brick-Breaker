using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;

namespace UI.Level
{
    public class LevelRaisedEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Audio _audio;

        private readonly CompositeDisposable _disposable = new();
        
        private void OnEnable()
        {
            MessageBrokerHolder.Game.Receive<M_LevelRaised>().Subscribe(message => HandleLevelRaised())
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