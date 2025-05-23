using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private float _startMinimum;
    [SerializeField] private float _startMaximum;
    [SerializeField] private float _timeToFill = 0.5f;

    [SerializeField] private TextMeshProUGUI _currentIndicator;

    public float CurrentCount => Current;
    protected float Current = 0;

    private Coroutine _smoothFill;

    protected float Maximum { get; set; }
    protected float Minimum { get;  set; }

    protected void OnDisable()
    {
        Disable();
    }

    protected virtual void Disable()
    {
        if (_smoothFill != null)
            StopCoroutine(_smoothFill);

        _smoothFill = null;
    }

    public virtual void ResetBar()
    {
        Minimum = _startMinimum;
        Maximum = _startMaximum;

        Fill();
    }

    protected virtual void Fill()
    {
        float currentFill = Current - Minimum;
        _currentIndicator.text = currentFill.ToString(CultureInfo.InvariantCulture);

        if (_smoothFill != null)
            StopCoroutine(_smoothFill);

        _smoothFill = StartCoroutine(SmoothFill(currentFill / Maximum));
    }

    private IEnumerator SmoothFill(float destination)
    {
        float currentTime = 0;

        while (currentTime < _timeToFill)
        {
            currentTime += Time.unscaledDeltaTime;
            _filler.fillAmount = Mathf.Lerp(_filler.fillAmount, destination, currentTime / _timeToFill);

            yield return null;
        }

        _smoothFill = null;
    }
}
