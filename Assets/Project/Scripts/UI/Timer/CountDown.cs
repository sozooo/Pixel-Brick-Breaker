using UnityEngine;
using System.Collections;
using DG.Tweening;
using Project.Scripts.WorkObjects.MessageBrokers;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private int _seconds = 3;
    [SerializeField] private int _countDownMinimum = 1;
    [SerializeField] private float _step = 0.9f;
    [SerializeField] private TextMeshProUGUI _text;
    
    [SerializeField] private float _fontSizeStep = 100f;
    [SerializeField] private float _fontSizeChangeDuration = 0.2f;
    [SerializeField] private Color _textTargetColor;
    
    private float _currentFontSize;
    private Tween _fontSizeTween;
    
    private void OnEnable()
    {
        StartCoroutine(StartCountDown());
    }
    
    private IEnumerator StartCountDown()
    {
        WaitForSecondsRealtime wait = new(_step);
    
        for (int i = _seconds; i >= _countDownMinimum; i--)
        {
            _text.text = i.ToString();
            
            ChangeFontSize(_text.fontSize + _fontSizeStep);
            
            if(i == _countDownMinimum)
                _text.DOColor(_textTargetColor, _fontSizeChangeDuration / 2);
    
            yield return wait;
        }
    
        ChangeFontSize(0);
    
        MessageBrokerHolder.Game.Publish(new M_GameStarted());
    }
    
    private void ChangeFontSize(float targetFontSize)
    {
        _fontSizeTween?.Kill();
            
        _fontSizeTween = DOTween
            .To(() => _text.fontSize, currentValue => _text.fontSize = currentValue, targetFontSize, _fontSizeChangeDuration)
            .SetUpdate(true)
            .SetEase(Ease.InOutSine).OnComplete(() => _fontSizeTween = null);
    }
}
