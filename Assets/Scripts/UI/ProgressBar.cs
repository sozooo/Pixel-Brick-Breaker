using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private float _minimum;
    [SerializeField] private float _maximum;
    [SerializeField] private float _timeToFill = 0.5f;

    [SerializeField] private TextMeshProUGUI _currentIndicator;

    [SerializeField] protected float Current = 0;

    private Coroutine _smoothFill;

    public float Maximum => _maximum;
    public float Minimum => _minimum;

    protected void OnEnable()
    {
        _currentIndicator.text = Current.ToString();

        Fill();
    }

    protected virtual void Fill()
    {
        float currentFill = Current - _minimum;
        _currentIndicator.text = currentFill.ToString();

        if (_smoothFill != null)
            StopCoroutine(_smoothFill);

        _smoothFill = StartCoroutine(SmoothFill(currentFill / _maximum));
    }

    protected virtual void IncreaseMaximum(float increaser)
    {
        SetNewMinimum(_maximum);
        _maximum += increaser;

        Fill();
    }

    private void SetNewMinimum(float minimum)
    {
        _minimum = minimum;
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