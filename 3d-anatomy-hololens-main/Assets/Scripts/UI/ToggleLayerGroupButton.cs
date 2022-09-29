using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLayerGroupButton : MonoBehaviour
{
	#region Private Fields
	//Serialized
	[Tooltip("Layer buttons that get toggled")]
	[SerializeField] private ToggleLayerButton[] _layerButtonsToToggle;
	[Tooltip("InteractableToggle")]
	[SerializeField] private Interactable _interactableButton;
	//Non-Serialized
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods

	private void Start()
	{

	}

	private void Update()
	{

	}

	#endregion Monobehavior Methods

	#region Private Methods
	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Callback for toggle button being clicked
	/// </summary>
	public void OnClick()
	{
		foreach (ToggleLayerButton toggleLayerButton in _layerButtonsToToggle)
		{
			toggleLayerButton.SetState(_interactableButton.IsToggled);
		}
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Resets state of button
	/// </summary>
	public void ResetButton()
	{
		_interactableButton.IsToggled = true;
		foreach (ToggleLayerButton toggleLayerButton in _layerButtonsToToggle)
		{
			toggleLayerButton.ResetButton();
		}
	}

	#endregion Events

}
