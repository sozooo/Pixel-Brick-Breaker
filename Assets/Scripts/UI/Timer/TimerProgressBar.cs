using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class TimerProgressBar : ProgressBar
{
    private float _fixedTime;

    public event Action TimePassed;
    public event Action<float> SecondPassed;

    public float FixedTime => _fixedTime;

    private new void OnEnable()
    {
        base.OnEnable();

        _fixedTime = Current;

        StartCoroutine(Timer());
    }

    [ProPlayButton]
    public void AddTime(float time)
    {
        _fixedTime = Current;

        Current += time;
        Current = Mathf.Clamp(Current, Minimum, Maximum);

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

            if (Current == Minimum)
                TimePassed?.Invoke();
        }
    }
}
