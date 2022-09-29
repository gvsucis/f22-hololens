using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Controllers/Active Hand Controller")]
    public class ActiveHandControllerSO : ScriptableObject
    {
        #region Private Fields

        [SerializeField] private EventChannelSO _onSwitchActiveHandEvent = default;
        [SerializeField] private EventChannelSO _onSwitchActiveCompleteEvent = default;
        [SerializeField] private HandReferencesSO _leftHand = default;
        [SerializeField] private HandReferencesSO _rightHand = default;
        private HandReferencesSO _activeHand;
        private HandReferencesSO _inactiveHand;
        #endregion Private Fields

        #region Monobehavior Methods

        private void OnEnable()
        {
            _activeHand = _leftHand;
            _inactiveHand = _rightHand;
            _onSwitchActiveHandEvent.OnEventRaised += OnSwitchActiveHand;
        }

		private void OnDisable()
        {
            _activeHand = _leftHand;
            _inactiveHand = _rightHand;
            _onSwitchActiveHandEvent.OnEventRaised -= OnSwitchActiveHand;
        }

		#endregion Monobehavior Methods

		#region Public Methods

        public HandReferencesSO GetActiveHand()
		{
            return _activeHand;
        }
        public HandReferencesSO GetInactiveHand()
        {
            return _inactiveHand;
        }

        public HandReferencesSO GetLeftHand()
        {
            return _leftHand;
        }

        public HandReferencesSO GetRightHand()
        {
            return _rightHand;
        }

        #endregion Public Methods

        #region Events

        private void OnSwitchActiveHand(CustomArgs arg0)
        {
            if(_activeHand == _leftHand)
			{
                _activeHand = _rightHand;
                _inactiveHand = _leftHand;
            }
            else if(_activeHand == _rightHand)
			{
                _activeHand = _leftHand;
                _inactiveHand = _rightHand;
            }
            _onSwitchActiveCompleteEvent.RaiseEvent();
        }

        #endregion Events
    }

    public enum HandSide
    {
        Left = 0,
        Right,
    }

}
