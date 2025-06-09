using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;

namespace WorkObjects.Handlers
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
                .Publish(new M_TimePassed());
        }
    }
}