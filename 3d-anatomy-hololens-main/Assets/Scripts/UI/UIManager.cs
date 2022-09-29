using CrossComm.Framework.ScriptableObjects;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

	#region Private Fields
	//Serialized
	[Tooltip("Gameobject parent of placing points UI")]
	[SerializeField] private GameObject _placeReferencePointParent;
	[Tooltip("Gameobject parent of Manipulating")]
	[SerializeField] private GameObject _adjustPointsParent;
	[Tooltip("GameObject parent of Reference Body Selection UI")]
	[SerializeField] private GameObject _selectReferenceBodyParent;
	[Tooltip("UI Backpanel")]
	[SerializeField] private GameObject _backPanel;
	[Tooltip("Text for reference point name")]
	[SerializeField] private TextMeshPro _referencePointText;
	//Non-Serialized
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Public Methods

	/// <summary>
	/// Resets state of all UI objects
	/// </summary>
	public void ResetUI()
	{
		//Reset blendshape sliders to 0;
		foreach (BlendShapeSlider slider in GetComponentsInChildren<BlendShapeSlider>(true))
		{
			slider.ResetSlider();
		}

		//Reset ToggleLayer buttons
		foreach (ToggleLayerButton button in GetComponentsInChildren<ToggleLayerButton>(true))
		{
			button.ResetButton();
		}

		//Reset other buttons
		foreach (UIButtonHandler button in GetComponentsInChildren<UIButtonHandler>(true))
		{
			button.Reset();
		}
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Callback for app state changing
	/// </summary>
	/// <param name="i_args"></param>
	public void OnAppStateChanged(CustomArgs i_args)
	{
		switch (i_args.GetObject<AppManager.State>())
		{
			case AppManager.State.ChooseReferenceMesh:
				_placeReferencePointParent.SetActive(false);
				_adjustPointsParent.SetActive(false);
				_selectReferenceBodyParent.SetActive(true);
				_backPanel.SetActive(false);
				break;
			case AppManager.State.PlaceReferencePoints:
				_placeReferencePointParent.SetActive(true);
				_adjustPointsParent.SetActive(false);
				_selectReferenceBodyParent.SetActive(false);
				_backPanel.SetActive(true);
				break;
			case AppManager.State.AdjustReferencePoints:
				_placeReferencePointParent.SetActive(false);
				_adjustPointsParent.SetActive(true);
				_selectReferenceBodyParent.SetActive(false);
				_backPanel.SetActive(true);
				break;
		}
	}


	/// <summary>
	/// Callback for new reference point being ready
	/// </summary>
	/// <param name="i_args"></param>
	public void OnNewReferencePointReady(CustomArgs i_args)
	{
		_referencePointText.text = i_args.GetObject<ReferencePoint>().GetName();
	}

	#endregion Events

}
