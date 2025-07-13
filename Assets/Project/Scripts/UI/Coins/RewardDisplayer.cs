using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Coins
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RewardDisplayer : MonoBehaviour
    {
        [SerializeField] protected RewardCollector Collector;
        [SerializeField] private float _lerpTime;

        protected float LastValue = 0;

        private TextMeshProUGUI _text;
        private Coroutine _lerping;
    
        public float LastValueSetted => LastValue;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected virtual void Display(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_lerping == null)
                StartCoroutine(Lerping(value));
        }

        private IEnumerator Lerping(float endValue)
        {
            float currentTime = 0;
            float currentValue = LastValue;

            while (currentTime < _lerpTime)
            {
                currentTime += Time.deltaTime;
                currentValue = Mathf.Lerp(currentValue, endValue, currentTime / _lerpTime);

                _text.text = ((int)currentValue).ToString();

                yield return null;
            }

            _lerping = null;
        }
    }
}
