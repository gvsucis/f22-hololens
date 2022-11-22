using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.OpenXR.Remoting;

public class HoloRemotingHandler : MonoBehaviour
{
	#region Private Fields

	[SerializeField] private GameObject _connectedUIParent;
	[SerializeField] private GameObject _disconnectedUIParent;
	[SerializeField] private TMP_InputField _ipInputField;
	[SerializeField] private TextMeshProUGUI _ipDisplay;
	[SerializeField, Tooltip("The configuration information for the remote connection.")]
    private RemotingConfiguration remotingConfiguration = new RemotingConfiguration { RemotePort = 8265, MaxBitrateKbps = 20000 };

	#endregion #region Private Fields

	#region Private Methods
	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Callback for UI Connect Button
	/// </summary>
	public void OnClickConnect()
	{
		_connectedUIParent.SetActive(true);
		_disconnectedUIParent.SetActive(false);
		remotingConfiguration.RemoteHostName = _ipInputField.text;
		_ipDisplay.text = _ipInputField.text;
		StartCoroutine(AppRemoting.Connect(remotingConfiguration));
	}

	/// <summary>
	/// Callback for UI Disconnect Button
	/// </summary>
	public void OnClickDisconnect()
	{
		_connectedUIParent.SetActive(false);
		_disconnectedUIParent.SetActive(true);
		AppRemoting.Disconnect();
	}

	#endregion Public Methods
}
