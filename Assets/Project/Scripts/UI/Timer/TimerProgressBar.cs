using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using YG;

public class TimerProgressBar : ProgressBar
{
    [SerializeField] private float _timeModifier = 5;
    
    private Coroutine _timer;

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
        
        _timer = StartCoroutine(Timer());
    }

    protected override void Disable()
    {
        base.Disable();
        
        if (_timer != null)
            StopCoroutine(_timer);
        
        _timer = null;
    }

    public override void ResetBar()
    {
        base.ResetBar();
        
        float upgradedTime = _timeModifier * YG2.saves.TimerLevel;
        Maximum += upgradedTime;
        Current = Maximum;
        
        Fill();
    }
    
    private IEnumerator Timer()
    {
        WaitForSeconds waitSecond = new(1f);

        while (Current > Minimum)
        {
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
