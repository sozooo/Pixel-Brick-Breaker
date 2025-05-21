using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] protected float StartMinimum;
    [SerializeField] protected float StartMaximum;
    [SerializeField] protected float StartCurrent;
    [SerializeField] private float _timeToFill = 0.5f;

    [SerializeField] private TextMeshProUGUI _currentIndicator;

    public float CurrentCount => Current;
    protected float Current = 0;

    private Coroutine _smoothFill;

    protected float Maximum { get; private set; }
    protected float Minimum { get; private set; }

    private void OnEnable()
    {
        ResetBar();
    }

    protected void OnDisable()
    {
        if (_smoothFill != null)
            StopCoroutine(_smoothFill);
    }

    protected virtual void ResetBar()
    {
        Minimum = StartMinimum;
        Maximum = StartMaximum;
        Current = StartCurrent;

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

    protected virtual void IncreaseMaximum(float increaser)
    {
        SetNewMinimum(Maximum);
        Maximum += increaser;

        Fill();
    }

    private void SetNewMinimum(float minimum)
    {
        Minimum = minimum;
    }

    private IEnumerator SmoothFill(float destination)
    {
        float currentTime = 0;

        while (currentTime < _timeToFill)
        {
            currentTime += Time.deltaTime;
            _filler.fillAmount = Mathf.Lerp(_filler.fillAmount, destination, currentTime / _timeToFill);

            yield return null;
        }

        _smoothFill = null;
    }

    
}
