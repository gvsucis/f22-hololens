using CrossComm.Framework.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GestureInputHelper : MonoBehaviour
{
    #region Data
    [Serializable]
    public class GestureInputPair
    {
        public ButtonStateValueSO _buttonStateSO;
        public GestureAction _gesture;
    }

    [Serializable]
    public class HandedGestureData
    {
        public HandSide _side;
        public GestureInputPair _inputPair;
    }
    #endregion

    #region Private Fields
    //Serialized
    [SerializeField] private HandedGestureData[] _gestureList;
    [SerializeField] private GlobalHandednessSO _handSideSO;
    //Non-Serialized

    private Dictionary<HandSide, GestureInputPair> _gestureDict = new Dictionary<HandSide, GestureInputPair>();
    #endregion Private Fields

    #region Public Fields
    #endregion Public Fields

    #region Monobehavior Methods

    private void Start()
    {
        foreach (HandedGestureData pair in _gestureList)
        {
            _gestureDict.Add(pair._side, pair._inputPair);
        }
    }

    private void LateUpdate()
    {
        UpdateSOStates();
    }

    #endregion Monobehavior Methods

    #region PUN Methods
    #endregion

    #region Private Methods
    /// <summary>
    /// Iterates through _buttonPairs List and updates the ButtonState SO.
    /// </summary>
    private void UpdateSOStates()
    {
        var pair = _gestureDict[_handSideSO._handSide];
        UpdateState(pair._gesture, pair._buttonStateSO);
    }

    /// <summary>
    /// Updates values in ButtonState ScriptableObjects. 
    /// </summary>
    /// <param name="i_actionReference">Input Action Reference from Unity Input System Input Map.</param>
    /// <param name="i_buttonStateSO">A reference to SO to insert values into.</param>
    private void UpdateState(GestureAction i_actionReference, ButtonStateValueSO i_buttonStateSO)
    {
        if (i_actionReference != null)
        {
            var lastValue = i_buttonStateSO.currentValue.Down || i_buttonStateSO.currentValue.Held;
            var currentValue = i_actionReference.GetGesture();
            i_buttonStateSO.currentValue.Params = i_actionReference.GetParams();

            i_buttonStateSO.currentValue.Down = currentValue && !lastValue;
            i_buttonStateSO.currentValue.Held = lastValue && currentValue;
            i_buttonStateSO.currentValue.Up = lastValue && !currentValue;
        }
    }
    #endregion Private Methods

    #region Public Methods
    #endregion Public Methods

    #region Coroutines
    #endregion Coroutines

    #region Events
    #endregion Events

}
