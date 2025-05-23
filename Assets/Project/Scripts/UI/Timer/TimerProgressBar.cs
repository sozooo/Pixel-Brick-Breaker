using System;
using System.Collections;
using System.Threading;
using com.cyborgAssets.inspectorButtonPro;
using Cysharp.Threading.Tasks;
using Project.Scripts.WorkObjects.MessageBrokers;
using UniRx;
using UnityEngine;
using YG;

public class TimerProgressBar : ProgressBar
{
    [SerializeField] private float _timeModifier = 5;
    
    private readonly CompositeDisposable _disposable = new();
    private Coroutine _timer;
    private bool _isPaused;

    public event Action TimePassed;
    public event Action<float> SecondPassed;

    [ProPlayButton]
    public void AddTime(float time)
    {
        Current += time;
        Current = Mathf.Clamp(Current, Minimum, Maximum);

        Fill();
    }
    
    public void StartTimer()
    {
        Disable();

        MessageBrokerHolder.Figure.Receive<M_FigureFell>().Subscribe(message => PauseTimer(true)).AddTo(_disposable);
        MessageBrokerHolder.Figure.Receive<M_FigureDespawned>().Subscribe(message => PauseTimer(false)).AddTo(_disposable);
        
        _timer = StartCoroutine(Timer());
    }

    protected override void Disable()
    {
        base.Disable();
        
        if (_timer != null)
            StopCoroutine(_timer);
        
        _timer = null;
        _disposable.Clear();
    }

    public override void ResetBar()
    {
        base.ResetBar();
        
        float upgradedTime = _timeModifier * YG2.saves.TimerLevel;
        Maximum += upgradedTime;
        Current = Maximum;
        
        Fill();
    }

    private void PauseTimer(bool isPaused)
    {
        _isPaused = isPaused;
    }
    
    private IEnumerator Timer()
    {
        WaitForSeconds waitSecond = new(1f);
        WaitUntil waitPause = new(() => _isPaused == false);

        while (Current > Minimum)
        {
            yield return waitPause;
            
            yield return waitSecond;

            Current--;
            Current = Mathf.Clamp(Current, Minimum, Maximum);

            Fill();

            SecondPassed?.Invoke(Current);

            if (Mathf.Approximately(Current, Minimum))
                TimePassed?.Invoke();
        }

        _timer = null;
    }
}
