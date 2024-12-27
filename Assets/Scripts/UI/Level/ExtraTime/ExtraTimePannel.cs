using System;
using UnityEngine;
using UnityEngine.UI;

public class ExtraTimePannel : Pannel
{
    [SerializeField] private TimerProgressBar _gameOverTimer;
    [SerializeField] private BuyButton _buyExtraTimeButton;
    [SerializeField] private WatchAdButton _watchAdButton;

    public event Action TimerPassed;
    public event Action TimeRedeemed;
    
    private new void OnEnable()
    {
        base.OnEnable();
        _gameOverTimer.Reset();

        _buyExtraTimeButton.Redeemed += AddTime;
        _watchAdButton.Redeemed += AddTime;
        _gameOverTimer.TimePassed += PassTimer;
    }

    private void OnDisable()
    {
        _buyExtraTimeButton.Redeemed -= AddTime;
        _watchAdButton.Redeemed -= AddTime;
        _gameOverTimer.TimePassed -= PassTimer;
    }

    private void AddTime()
    {
        TimeRedeemed?.Invoke();
    }

    private void PassTimer()
    {
        TimerPassed?.Invoke();
    }
}