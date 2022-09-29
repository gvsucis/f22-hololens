using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
    public abstract class BasePersistentCollectionSO<T> : BaseCollectionSO<T>
    {

		#region Public Methods

		/// <summary>
		/// Add to collection on when not in play mode
		/// </summary>
		/// <param name="i_value"></param>
		/// <returns></returns>
		public override T AddToCollection(T i_value)
		{
			if(Application.isPlaying)
			{
				Debug.LogError($"Not Allowed to add {i_value} to a persistant collection. Use runtime collection if you need to add values at runtime");
				return i_value;
			}
			else
			{
				return base.AddToCollection(i_value);
			}
		}

		/// <summary>
		/// Only remove from collection when not in play mode
		/// </summary>
		/// <param name="i_value"></param>
		public override void RemoveFromCollection(T i_value)
		{
			if (Application.isPlaying)
			{
				Debug.LogError($"Not Allowed to remove {i_value} to a persistant collection. Use runtime collection if you need to add values at runtime");
			}
			else
			{
				base.RemoveFromCollection(i_value);
			}			
		}

		#endregion Public Methods
	}
}
