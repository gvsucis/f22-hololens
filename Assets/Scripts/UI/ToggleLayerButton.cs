using CrossComm.Framework.ScriptableObjects;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLayerButton : MonoBehaviour
{

	#region Private Fields
	//Serialized
	[Tooltip("Layer SO that is connected to Layer")]
	[SerializeField] private LayerSO _layerToToggle;
	[Tooltip("Event to send toggle event")]
	[SerializeField] private EventChannelSO _OnSetLayerStateEvent;
	//Non-Serialized
	//All layers toggled on by default
	private bool _isToggled = true;
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods
	#endregion Monobehavior Methods

	#region Private Methods
	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Sets state of UI and layer
	/// </summary>
	/// <param name="i_state"></param>
	public void SetState(bool i_state)
	{
		_isToggled = i_state;
		_OnSetLayerStateEvent.RaiseEvent(_layerToToggle, _isToggled);
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Callback for button clicked
	/// </summary>
	public void OnClick()
	{
		_isToggled = !_isToggled;
		_OnSetLayerStateEvent.RaiseEvent(_layerToToggle, _isToggled);
	}

	/// <summary>
	/// Resets state of button to on
	/// </summary>
	public void ResetButton()
	{
		_isToggled = true;
	}

	#endregion Events

}
