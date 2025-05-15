using UnityEngine;
using YG;

public class PausePannel : Pannel
{
    private float _timeScale;
    
    private void Awake()
    {
        _timeScale = Time.timeScale;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        YG2.PauseGame(true);
    }

    private void OnDisable()
    {
        Time.timeScale = _timeScale;
        YG2.PauseGame(false);
    }
}