using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonHandler : MonoBehaviour
{

	#region Private Fields
	//Serialized
	[SerializeField] private GameObject _toggle1;
	[SerializeField] private GameObject _toggle2;
	//Non-Serialized
	private bool _isToggled = false;
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

	//callback for button being clicked
	public void OnClick()
	{
		_isToggled = !_isToggled;
		_toggle1?.SetActive(!_isToggled);
		_toggle2?.SetActive(_isToggled);
	}

	/// <summary>
	/// Resetrs to original state
	/// </summary>
	public void Reset()
	{
		_isToggled = false;
		_toggle1?.SetActive(true);
		_toggle2?.SetActive(false);
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events
	#endregion Events

}
