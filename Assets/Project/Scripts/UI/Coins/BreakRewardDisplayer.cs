using System;

namespace Project.Scripts.UI.Coins
{
    public class BreakRewardDisplayer : RewardDisplayer
    {
        private float _fixedValue;

        private void OnEnable()
        {
            Collector.CurrentRewardChanged += Display;
        }

        private void OnDisable()
        {
            Collector.CurrentRewardChanged -= Display;
        }

        protected override void Display(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();

            float result = value - _fixedValue;
            _fixedValue = value;

            base.Display(result);
        }
    }
}
