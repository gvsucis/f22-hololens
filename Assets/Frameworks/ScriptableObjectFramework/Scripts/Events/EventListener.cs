using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CrossComm.Framework.ScriptableObjects
{
	public class EventListener : MonoBehaviour
	{
		[SerializeField] private List<EventChannel> _eventChannels = default;

		private void OnEnable()
		{
			foreach (EventChannel eventChannel in _eventChannels)
			{
				if (eventChannel.channel != null)
				{
					eventChannel.channel.OnEventRaised += eventChannel.Respond;
				}
			}
		}

		private void OnDisable()
		{

			foreach (EventChannel eventChannel in _eventChannels)
			{
				if (eventChannel.channel != null)
				{
					eventChannel.channel.OnEventRaised -= eventChannel.Respond;
				}
			}
		}
	}

	[Serializable]
	public class EventChannel
	{
		public EventChannelSO channel = default;
		[SerializeField] UnityEvent<CustomArgs> OnEventRaised = default;
		public void Respond(CustomArgs i_args)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(i_args);
		}
	}
}