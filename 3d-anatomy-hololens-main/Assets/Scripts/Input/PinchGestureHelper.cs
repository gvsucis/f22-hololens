using CrossComm.Framework.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchGestureHelper : MonoBehaviour
{

	#region Private Fields
	//Serialized
	[Tooltip("Hold time to complete pinch gesture")]
	[SerializeField] private float _pinchGestureHoldTime;
	[Tooltip("UI Image for filling progress bar")]
	[SerializeField] private Image _pinchProgressImage;
	[Tooltip("Event to call when pinch progress is complete")]
	[SerializeField] private EventChannelSO _pinchProgressCompleteEvent;
	//Non-Serialized
	private float _counter;
	private bool _isEnabled;
	private bool _pinchComplete;
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods
	#endregion Monobehavior Methods

	#region Private Methods

	/// <summary>
	/// Hanldes pinch being held
	/// </summary>
	private void HandlePinchHeld(Vector3 i_pos)
	{
		if (!_isEnabled || _pinchComplete)
		{
			return;
		}
		_pinchProgressImage.enabled = true;
		_pinchProgressImage.transform.position = i_pos;
		_counter += Time.deltaTime;
		float t = _counter / _pinchGestureHoldTime;
		_pinchProgressImage.fillAmount = t;

		if (t >= 1f)
		{
			CompletePinch(i_pos);
		}
	}

	/// <summary>
	/// Resets pinch variables
	/// </summary>
	private void ResetPinch()
	{
		_pinchComplete = false;
		_pinchProgressImage.enabled = false;
		_counter = 0f;
	}

	/// <summary>
	/// Handles completing the pinch gesture
	/// </summary>
	private void CompletePinch(Vector3 i_pos)
	{
		_pinchProgressImage.enabled = false;
		_pinchComplete = true;
		_pinchProgressCompleteEvent.RaiseEvent(i_pos);
	}

	#endregion Private Methods

	#region Public Methods
	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Callback for pinch held
	/// </summary>
	/// <param name="i_args"></param>
	public void OnPinchHeld(CustomArgs i_args)
	{
		HandlePinchHeld(i_args.GetObject<Transform>().position);
	}

	/// <summary>
	/// Callback for pinch released
	/// </summary>
	/// <param name="i_args"></param>
	public void OnPinchReleased(CustomArgs i_args)
	{
		ResetPinch();
	}

	/// <summary>
	/// Callback for app state changing
	/// </summary>
	/// <param name="i_args"></param>
	public void OnAppStateChanged(CustomArgs i_args)
	{
		switch (i_args.GetObject<AppManager.State>())
		{
			case AppManager.State.PlaceReferencePoints:
				_isEnabled = true;
				break;
			case AppManager.State.AdjustReferencePoints:
				_isEnabled = false;
				break;
		}
	}

	#endregion Events

}
