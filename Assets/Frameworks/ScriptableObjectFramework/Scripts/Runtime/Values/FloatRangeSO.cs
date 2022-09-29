using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Runtime Values/Float Range")]
    public class FloatRangeSO : ScriptableObject
    {
        public FloatValueSO minValue;
        public FloatValueSO maxValue;

        public float Range
        {
            get
            {
                return maxValue - minValue;
            }
        }

        public float GetClampedValue(float i_value)
        {
            return Mathf.Clamp(i_value, minValue, maxValue);
        }

        public float ClampedNormalValueInRange(float i_currentValue)
        {
            var value = Mathf.Clamp(i_currentValue, minValue, maxValue);

            return Mathf.Abs(value / Range);
        }

        public float NormalValueInRange(float i_currentValue)
        {
            return Mathf.Abs(i_currentValue / Range);
        }

        public float ValueFromNormal(float normalValue)
        {
            return Mathf.LerpUnclamped(minValue, maxValue, normalValue);
        }

        public float ClampedValueFromNormal(float normalValue)
        {
            return Mathf.Lerp(minValue, maxValue, normalValue);
        }

        public bool ValueInRange(float i_value)
        {
            return i_value <= maxValue && i_value >= minValue;
        }
    }
}
