using CrossComm.Framework.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

public class EventOnStateChange : MonoBehaviour
{

    #region Private Fields
    //Serialized
    [SerializeField] private AppManager.State _stateToTriggerEvent;
    [SerializeField] private EventChannelSO _stateChangeEvent;
    [SerializeField] private UnityEvent _event;
    //Non-Serialized
    #endregion Private Fields

    #region Public Fields
    #endregion Public Fields

    #region Monobehavior Methods

    private void Start()
    {
        _stateChangeEvent.OnEventRaised += CheckStateChange;
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
    public void CheckStateChange(CustomArgs i_args)
    {
        var newState = i_args.GetObject<AppManager.State>();

        if (newState.Equals(_stateToTriggerEvent))
        {
            _event.Invoke();
        }
    }
    #endregion Events

}
