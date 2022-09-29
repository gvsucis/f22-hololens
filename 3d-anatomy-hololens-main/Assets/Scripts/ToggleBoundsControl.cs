using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBoundsControl : MonoBehaviour
{

    #region Private Fields
    //Serialized
    [Tooltip("Colliders for control points to toggle with BoundsControl.")]
    [SerializeField] private List<Collider> _collidersToToggle;
    [Tooltip("Reference to BoundsControl component to toggle.")]
    [SerializeField] private BoundsControl _boundsControl;
    [Tooltip("Reference to ObjectManipulator to toggle with BoundsControl.")]
    [SerializeField] private ObjectManipulator _manipulator;
    [Tooltip("Reference to BoxCollider used by ObjectManipulator and BoundsControl. Toggles with BoundsControl.")]
    [SerializeField] private BoxCollider _boxCollider;
    //Non-Serialized
    #endregion Private Fields

    #region Public Fields
    #endregion Public Fields

    #region Monobehavior Methods

    private void Start()
    {

    }

    private void Update()
    {

    }

    #endregion Monobehavior Methods

    #region PUN Methods
    #endregion

    #region Private Methods
    #endregion Private Methods

    #region Public Methods
    #endregion Public Methods

    #region Coroutines
    #endregion Coroutines

    #region Events
    /// <summary>
    /// Toggles Bounds Control on/off. Also enables/disables other components 
    /// and colliders, so nothing weird happens. (e.g. makes sure that control
    /// points aren't usable while moving the body)
    /// </summary>
    public void Toggle()
    {
        _boundsControl.enabled = !_boundsControl.enabled;
        _manipulator.enabled = _boundsControl.enabled;
        _boxCollider.enabled = _boundsControl.enabled;
        foreach (var collider in _collidersToToggle)
        {
            collider.enabled = !_boundsControl.enabled;
        }
    }

    #endregion Events

}
