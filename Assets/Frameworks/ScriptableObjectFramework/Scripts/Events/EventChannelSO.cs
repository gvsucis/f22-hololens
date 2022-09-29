using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CrossComm.Framework.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Events/Event Channel")]
	public class EventChannelSO : ScriptableObject
	{
		[TextArea] [SerializeField] private string _description = "Add event description and type of passed Arguments";

		public UnityAction<CustomArgs> OnEventRaised;//Only for local unity actions

		[SerializeField] private bool _printLogs = true;

		//Network Specific
		public UnityAction<CustomArgs, EventChannelSO> OnNetworkedEventRaised; /// Only used by the PhotonRaiseEventManager for networked event calls
		[HideInInspector] public bool useLocalUnityAction = true;

		public void RaiseEvent(params object[] i_params)
		{
			CustomArgs args = new CustomArgs(i_params);

			if (OnNetworkedEventRaised != null && !useLocalUnityAction)
			{
				//If PhotonRaisEventManager is listening to this event, only call the PhotonRaiseEventManager Listener and return
				// THis is done so that the event can be called based on if 'All' or 'Others' is selected in RaiseEventOptions
				LogEvent(true, i_params, true);
				OnNetworkedEventRaised.Invoke(args, this);
				return;
			}

			if (OnEventRaised != null)
			{
				LogEvent(true, i_params);
				OnEventRaised.Invoke(args);
			}
			else
			{
				LogEvent(false);
			}
		}

		public void RaiseEvent()
		{
			if (OnNetworkedEventRaised != null && !useLocalUnityAction)
			{
				//If PhotonRaisEventManager is listening to this event, only call the PhotonRaiseEventManager Listener and return
				// THis is done so that the event can be called based on if 'All' or 'Others' is selected in RaiseEventOptions
				LogEvent(true, i_isNetworked: true);
				OnNetworkedEventRaised.Invoke(null, this);
				return;
			}

			if (OnEventRaised != null)
			{
				LogEvent(true);
				OnEventRaised.Invoke(null);
			}
			else
			{
				LogEvent(false);
			}
		}

		private void LogEvent(bool i_listenersAvailable, object[] i_params = null, bool i_isNetworked = false)
		{
			if (_printLogs)
			{
				if (i_listenersAvailable)
				{
					string networkedMessage = i_isNetworked ? "Networked" : string.Empty;

					string paramsLog = "-> Params : ";
					if (i_params != null)
					{
						foreach (object param in i_params)
						{
							paramsLog += $" | {param.ToString()}";
						}
					}
					else
					{
						paramsLog = string.Empty;
					}

					Debug.Log($"<color=green>{networkedMessage}Event Raised: {this.name} {paramsLog} </color>", this);
				}
				else
				{
					Debug.LogWarning($"Event raised : {this.name} but nothing is listening", this);
				}
			}
		}
	}

	public class CustomArgs : EventArgs
	{
		protected object[] _passedObjects;
		public CustomArgs(params object[] i_objects)
		{
			_passedObjects = i_objects;
		}

		public T GetObject<T>(int i_index = 0)
		{
			if (_passedObjects == null || _passedObjects.Length == 0)
			{
				Debug.LogError("No parameters were passed, but you're trying to access one");
				return default(T);
			}

			if (i_index >= _passedObjects.Length)
			{
				Debug.LogError($"No parameters present at index {i_index}");
				return default(T);
			}

			if (typeof(T) != _passedObjects[i_index].GetType())
			{
				Debug.LogError($"Passed Type '{_passedObjects[i_index].GetType()}' doesn't match recieved Type '{typeof(T)}'");
				return default(T);
			}
			return (T)_passedObjects[i_index];
		}

		public int GetParamsCount()
		{
			if (_passedObjects == null)
			{
				return 0;
			}

			return _passedObjects.Length;
		}

		public object[] GetPassedObjects()
		{
			return _passedObjects;
		}

		public void SetPassedObjects(object[] i_params)
		{
			_passedObjects = i_params;
		}
	}
}
