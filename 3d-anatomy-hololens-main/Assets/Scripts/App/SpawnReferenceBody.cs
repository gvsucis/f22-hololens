using CrossComm.Framework.ScriptableObjects;
using UnityEngine;

public class SpawnReferenceBody : MonoBehaviour
{

    #region Private Fields
    //Serialized
    [SerializeField]
    private EventChannelSO _eventChannel;
    //Non-Serialized
    #endregion Private Fields

    #region Events
    public void SpawnMesh(ReferenceMeshSO i_referenceMesh)
    {
        _eventChannel.RaiseEvent(i_referenceMesh);
    }
    #endregion Events

}
