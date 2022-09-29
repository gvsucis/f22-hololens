using CrossComm.Framework.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "Reference Body Event Channel")]

public class ReferenceBodyEventChannelSO : EventChannelSO
{



    #region Public Methods
    public void RaiseBodyEvent(ReferenceMeshSO i_mesh)
    {
        base.RaiseEvent(i_mesh);
    }
    #endregion Public Methods



}
