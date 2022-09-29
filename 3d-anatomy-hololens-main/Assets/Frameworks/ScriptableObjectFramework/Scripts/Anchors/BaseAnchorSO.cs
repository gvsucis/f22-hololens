using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
	public class BaseAnchorSO<T> : ScriptableObject
	{
		[TextArea] [SerializeField] private string _description;

		public bool isSet = false; // Any script can check if the transform is null before using it, by just checking this bool

		[SerializeField] private T _value;
		public T value
		{
			get
			{
				return _value;
			}
			set
			{
				if (isSet)
				{
					Debug.LogWarning($"Something is trying to override {this.name}. Transform Anchors should only be set once");
					return;
				}				
				_value = value;
				isSet = value != null;				
			}
		}
		private void OnEnable()
		{
			_value = default;
			isSet = false;
		}

		private void OnDisable()
		{
			_value = default;
			isSet = false;
		}
	}
}
