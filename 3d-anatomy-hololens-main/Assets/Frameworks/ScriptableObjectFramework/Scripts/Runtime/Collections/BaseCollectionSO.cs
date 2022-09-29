using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
	public abstract class BaseCollectionSO<T> : ScriptableObject
	{
		#region Private Fields
		//Serialized
		[TextArea(15, 20)]
		[SerializeField] protected List<T> _collection;
		//Non-Serialized
		#endregion Private Fields

		protected virtual void OnEnable()
		{

		}

		protected virtual void OnDisable()
		{

		}

		#region Public Methods

		/// <summary>
		/// Returns collection
		/// </summary>
		/// <returns></returns>
		public List<T> GetCollection()
		{
			return _collection;
		}

		/// <summary>
		/// Add value to collection
		/// </summary>
		/// <param name="i_value"></param>
		/// <returns></returns>
		public virtual T AddToCollection(T i_value)
		{
			if (_collection == null)
			{
				_collection = new List<T>();
			}

			_collection.Add(i_value);
			return i_value;
		}

		/// <summary>
		/// remove value from collection
		/// </summary>
		/// <param name="i_value"></param>
		public virtual void RemoveFromCollection(T i_value)
		{
			if (_collection.Contains(i_value))
			{
				_collection.Remove(i_value);
			}
		}

		#endregion Public Methods
	}
}
