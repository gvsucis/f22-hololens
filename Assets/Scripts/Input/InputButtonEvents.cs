using CrossComm.Framework.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;


public class InputButtonEvents : MonoBehaviour
{
	#region Data
	[System.Serializable]
	public class ButtonEventData
	{
		public string Name;
		public ButtonStateValueSO _buttonStateValueSO;
		public EventChannelSO _onButtonPressed;
		public EventChannelSO _onButtonHeld;
		public EventChannelSO _onButtonReleased;

		public void Deconstruct(out ButtonStateValueSO buttonStateValueSO, out EventChannelSO onButtonPressed, out EventChannelSO onButtonHeld, out EventChannelSO onButtonReleased)
		{
			buttonStateValueSO = _buttonStateValueSO;
			onButtonPressed = _onButtonPressed;
			onButtonHeld = _onButtonHeld;
			onButtonReleased = _onButtonReleased;
		}
	}
	#endregion

	#region Private Fields
	//Serialized
	[SerializeField] private List<ButtonEventData> _inputEntries = new List<ButtonEventData>();
	//Non-Serialized
	#endregion Private Fields

	#region Monobehavior Methods

	private void Start()
	{

	}

	private void LateUpdate()
	{
		foreach (var data in _inputEntries)
		{
			var (_buttonStateValueSO, onButtonPressed, onButtonHeld, onButtonReleased) = data;

			if (_buttonStateValueSO.currentValue.Held && onButtonHeld)
			{
				onButtonHeld.RaiseEvent(_buttonStateValueSO.currentValue.Params);
			}

			if (_buttonStateValueSO.currentValue.Down && onButtonPressed)
			{
				onButtonPressed.RaiseEvent(_buttonStateValueSO.currentValue.Params);
			}

			if (_buttonStateValueSO.currentValue.Up && onButtonReleased)
			{
				onButtonReleased.RaiseEvent(_buttonStateValueSO.currentValue.Params);
			}
		}
	}

	#endregion Monobehavior Methods
}
