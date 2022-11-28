using CrossComm.Framework.ScriptableObjects;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public enum State
    {
        ChooseReferenceMesh,
        PlaceReferencePoints,
        AdjustReferencePoints,
    }


    #region Private Fields
    //Serialized
    [Tooltip("Current mesh to use")]
    [SerializeField] private MeshHandler _currentMesh;
    [Tooltip("Event to call when app state has changed")]
    [SerializeField] private EventChannelSO _onAppStateChangedEvent;
    [Tooltip("Event to call when next reference point is ready")]
    [SerializeField] private EventChannelSO _onNewReferencePointEvent;
    //Non-Serialized
    private State _currentState;
    private int _currentPlacementIndex = 0;
    private GameObject _currentSpawnedBody;
    #endregion Private Fields

    #region Public Fields
    public State CurrentState
    {
        get { return _currentState; }
    }
    #endregion Public Fields

    #region Monobehavior Methods

    private void Start()
    {
        SetState(State.ChooseReferenceMesh);
    }

    #endregion Monobehavior Methods

    #region Private Methods

    private void SetState(State i_state)
    {
        Debug.Log($"Setting State to {i_state}");
        _currentState = i_state;
        switch (_currentState)
        {
            case State.ChooseReferenceMesh:
                break;
            case State.PlaceReferencePoints:
                //Disable layers
                _currentMesh.SetMeshState(false);
                //send event to a UI manager to show which part we're currently placing
                _onNewReferencePointEvent.RaiseEvent(_currentMesh.GetReferencePoint(_currentPlacementIndex));
                break;
            case State.AdjustReferencePoints:
                //Enable layers
                _currentMesh.SetMeshState(true);
                break;
        }
        _onAppStateChangedEvent.RaiseEvent(_currentState);
    }

    /// <summary>
    /// Places next reference point at given position
    /// </summary>
    private void PlaceNextReferencePoint(Vector3 i_position)
    {
        if (_currentState != State.PlaceReferencePoints)
        {
            return;
        }

        // Check if first point and set rotation and position of entire mesh accordingly
        if (_currentPlacementIndex == 0)
        {
            _currentMesh.SetMeshPosition(i_position);
        }

        ReferencePoint currentReferencePoint = _currentMesh.GetReferencePoint(_currentPlacementIndex);
        currentReferencePoint.transform.position = i_position;
        currentReferencePoint.SetReferencePointState(true);
        _currentPlacementIndex++;


        //send event to a UI manager to show which part we're currently placing
        if (_currentMesh.DoesReferencePointExist(_currentPlacementIndex))
        {
            _onNewReferencePointEvent.RaiseEvent(_currentMesh.GetReferencePoint(_currentPlacementIndex));
        }
        else
        {
            //Set adjust state
            SetState(State.AdjustReferencePoints);
        }

    }

    /// <summary>
    /// Resets to placing reference points
    /// </summary>
    private void Reset()
    {
        _currentPlacementIndex = 0;
        SetState(State.ChooseReferenceMesh);
    }

    #endregion Private Methods

    #region Public Methods

    public void OnConnected() {
        
    }

    #endregion Public Methods

    #region Coroutines
    #endregion Coroutines

    #region Events

    /// <summary>
    /// Callback for selecting a reference mesh
    /// </summary>
    /// <param name="i_args"></param>
    public void OnReferenceMeshSelected(CustomArgs i_args)
    {
        var bodyToSpawn = i_args.GetObject<ReferenceMeshSO>();
        _currentSpawnedBody = GameObject.Instantiate(bodyToSpawn.referenceMeshPrefab);
        _currentMesh = _currentSpawnedBody.GetComponent<MeshHandler>();
        SetState(State.PlaceReferencePoints);
    }

    /// <summary>
    /// Callback for when reference mesh is spawned
    /// </summary>
    /// <param name="i_args"></param>
    public void OnReferenceMeshSpawned(CustomArgs i_args)
    {

    }

    /// <summary>
    /// Callback for when App is reset to reference body selection.
    /// </summary>
    public void OnReset()
    {
        Destroy(_currentSpawnedBody);
        Reset();
    }

    /// <summary>
    /// Callback for when place gesture is called
    /// </summary>
    public void OnPlaceGestureReceived(CustomArgs i_args)
    {
        //place reference point at the vector 3 passed in the params
        PlaceNextReferencePoint(i_args.GetObject<Vector3>());
    }
    #endregion Events
}
