using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

namespace CrossComm.Framework.ScriptableObjects
{
	public class ScriptableObjectEventsSample : MonoBehaviour
	{
		[SerializeField] private EventChannelSO _customEventChannel = default;
		[SerializeField] private TransformAnchorSO _transformAnchor = default; // Can be used every where without referencing script
		[SerializeField] private IntValueSO _intSample = default;

		private void Awake()
		{
			_transformAnchor.value = this.transform;
			_intSample.currentValue = _intSample + 5;
			Debug.Log(_intSample);
		}

		[Button]
		private void RaiseCustomEvent()
		{
			_customEventChannel.RaiseEvent("TESTING", 1);
		}

		public void OnCustomEventRaised(CustomArgs i_args)
		{
			Debug.Log("Event Raised");
			Debug.Log(i_args.GetObject<string>(0));
			Debug.Log(i_args.GetObject<int>(1));
		}
	}
}