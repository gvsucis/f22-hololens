using CrossComm.Framework.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "HandSide")]
public class GlobalHandednessSO : ScriptableObject
{
    [Tooltip("Current active HandSide")]
    public HandSide _handSide;

    /// <summary>
    /// Toggles Active HandSideValue.
    /// </summary>
    public void ToggeHandSide()
    {
        _handSide = (_handSide == HandSide.Left) ? HandSide.Right : HandSide.Left;
    }
}
