using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class TimerProgressBar : ProgressBar
{
    public event Action TimePassed;

    private new void OnEnable()
    {
        Current = Maximum;
        StartCoroutine(Timer());

        base.OnEnable();
    }

    [ProPlayButton]
    public void AddTime(float time)
    {
        Current += time;
        Current = Mathf.Clamp(Current, Minimum, Maximum);

        Fill();
    }

    private IEnumerator Timer()
    {
        WaitForSeconds wait = new(1);

        while (Current > Minimum)
        {
            yield return wait;

            Current--;
            Current = Mathf.Clamp(Current, Minimum, Maximum);

            Fill();

            if (Current == Minimum)
                TimePassed?.Invoke();
        }
    }
}
