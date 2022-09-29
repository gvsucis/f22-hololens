using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{    public class AssignToCanvasCollection : MonoBehaviour
    {

        #region Private Fields
        //Serialized
        [SerializeField] private CanvasCollectionSO _canvasCollection = default;
        //Non-Serialized
        #endregion Private Fields

        #region Monobehavior Methods

        private void Start()
        {
            _canvasCollection.AddToCollection(GetComponent<Canvas>());
        }

		private void OnDestroy()
		{
            _canvasCollection.RemoveFromCollection(GetComponent<Canvas>());
        }

		#endregion Monobehavior Methods


	}
}


