using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
    [CreateAssetMenu(menuName = "References/Hand References")]
    public class HandReferencesSO : ScriptableObject
    {

        #region Public Fields

        public HandSide handSide;
        public TransformAnchorSO handAnchor;
        public TransformAnchorSO drawPointAnchor;
        public TransformAnchorSO indexBaseAnchor;
        public ButtonStateValueSO primaryButtonState;
        public ButtonStateValueSO secondaryButtonState;
        public ButtonStateValueSO triggerButtonState;
        public ButtonStateValueSO thumbstickUpState;
        public ButtonStateValueSO thumbstickLeftState;
        public ButtonStateValueSO thumbstickRightState;
        public ButtonStateValueSO thumbstickState;
        public ButtonStateValueSO thumbstickTouch;
        public ButtonStateValueSO triggerTouch;
        public ButtonStateValueSO grabState;

        #endregion Private Fields

    }
}

