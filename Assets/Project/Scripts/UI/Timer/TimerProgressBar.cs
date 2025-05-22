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

    private void OnDisable()
    {
        if (_timer == null)
            return;
        
        StopCoroutine(_timer);
        _timer = null;
    }

    [ProPlayButton]
    public void AddTime(float time)
    {
        Current += time;
        Current = Mathf.Clamp(Current, Minimum, Maximum);

        Fill();
    }
    
    public void StartTimer()
    {
        _timer ??= StartCoroutine(Timer());
    }

    protected override void ResetBar()
    {
        float upgradedTime = _timeModifier * YG2.saves.TimerLevel;
        StartMaximum += upgradedTime;
        StartCurrent = StartMaximum;

        base.ResetBar();
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
