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

    public float BonusTime { get; private set; }

    private new void OnEnable()
    {
        BonusTime = Current;

        float upgradedTime = _timeModifier * YG2.saves.TimerLevel;
        StartMaximum += upgradedTime;
        StartCurrent = StartMaximum;

        base.ResetBar();
    }

    [ProPlayButton]
    public void AddTime(float time)
    {
        BonusTime = Current;
        
        Current += time;
        Current = Mathf.Clamp(Current, Minimum, Maximum);

        Fill();
    }

    public override void ResetBar()
    {
        if(_timer != null)
        {
            StopCoroutine(_timer);
            _timer = null;
        }

        BonusTime = Current;

        base.ResetBar();

        _timer = StartCoroutine(Timer());
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
    }
}
