using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.UI.Timer;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using Project.Scripts.WorkObjects.MessageBrokers.Game;
using UniRx;

namespace Project.Scripts.WorkObjects.Handlers
{
    [Serializable]
    public class TimerHandler
    {
        private TimerProgressBar _timer;
        
        private float _bonusTime;

        public TimerHandler(TimerProgressBar timer)
        {
            _timer = timer;
        }

        public void Initialize(float bonusTime, CancellationToken token)
        {
            MessageBrokerHolder.Game
                .Receive<M_GameStarted>()
                .Subscribe(_ => StartTimer())
                .AddTo(token);
            
            MessageBrokerHolder.Figure
                .Receive<M_FigureFell>()
                .Subscribe(_ => AddTime())
                .AddTo(token);
            
            _bonusTime = bonusTime;
            
            _timer.ResetBar();
            
            _timer.TimePassed += OnTimePassed;
        }

        public void Disable()
        {
            _timer.TimePassed -= OnTimePassed;
        }
        
        private void StartTimer()
        {
            _timer.StartTimer();
        }

        private void AddTime()
        {
            MessageBrokerHolder.Game
                .Publish(new M_TimeRedeemed(_bonusTime));
        }

        private void OnTimePassed()
        {
            MessageBrokerHolder.Game
                .Publish(default(M_TimePassed));
        }
    }
}