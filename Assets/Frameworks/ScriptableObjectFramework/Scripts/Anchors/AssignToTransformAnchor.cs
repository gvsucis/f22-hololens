using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
	public class AssignToTransformAnchor : MonoBehaviour
	{
		#region Private Fields

		[SerializeField] private TransformAnchorSO _transformAnchor = default;

		#endregion Private Fields

		#region Monobehavior Methods

		private void Awake()
		{
			_transformAnchor.value = this.transform;
		}
		#endregion Monobehavior Methods     

	}

}
