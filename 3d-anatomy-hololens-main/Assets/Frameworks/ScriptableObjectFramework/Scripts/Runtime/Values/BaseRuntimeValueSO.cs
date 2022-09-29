using UnityEngine;
using UnityEngine.Events;

namespace CrossComm.Framework.ScriptableObjects
{
    public abstract class BaseRuntimeValueSO<T> : ScriptableObject
    {
        public T defaultValue;

        [SerializeField] private T _currentValue;
        [SerializeField] private UnityEvent _onValueChanged;

        public T currentValue
        {
            get { return _currentValue; }
            set { _currentValue = value; _onValueChanged.Invoke(); }
        }

        private void OnEnable()
        {
            _currentValue = defaultValue;
        }

        /// <summary>
        /// Now current value can be directly referenced
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator T(BaseRuntimeValueSO<T> a) => (T)a.currentValue;

        public override string ToString() => currentValue.ToString();
    }
}
