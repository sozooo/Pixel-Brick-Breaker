using System;
using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;

namespace WorkObjects.Handlers
{
    [Serializable]
    public class TimerHandler
    {
        private TimerProgressBar _timer;
        
        private float _bonusTime;
        
        private readonly CompositeDisposable _disposable = new();

        public TimerHandler(TimerProgressBar timer)
        {
            _timer = timer;
        }

        public void Initialize(float bonusTime)
        {
            MessageBrokerHolder.Game.Receive<M_GameStarted>().Subscribe(message => StartTimer()).AddTo(_disposable);
            MessageBrokerHolder.Figure.Receive<M_FigureFell>().Subscribe(message => AddTime()).AddTo(_disposable);
            
            _bonusTime = bonusTime;
            
            _timer.ResetBar();
            _timer.TimePassed += OnTimePassed;
        }

        public void Disable()
        {
            _disposable.Clear();
        
            _timer.TimePassed -= OnTimePassed;
        }
        
        private void StartTimer()
        {
            _timer.StartTimer();
        }

        private void AddTime()
        {
            MessageBrokerHolder.Game.Publish(new M_TimeRedeemed(_bonusTime));
        }

        private void OnTimePassed()
        {
            MessageBrokerHolder.Game.Publish(new M_TimePassed());
        }
    }
}