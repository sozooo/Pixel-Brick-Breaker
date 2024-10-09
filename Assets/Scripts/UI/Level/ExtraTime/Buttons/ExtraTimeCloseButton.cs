using UnityEngine;

public class ExtraTimeCloseButton : CloseButton
{
    [SerializeField] private ExtraTimeManager _extraTimeManager;

    protected override void Close()
    {
        _extraTimeManager.EndGame();
        base.Close();
    }
}