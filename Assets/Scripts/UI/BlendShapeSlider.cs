using CrossComm.Framework.ScriptableObjects;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeSlider : MonoBehaviour
{

	#region Data

	[System.Serializable]
	private class LayerBlendShape
	{
		public LayerSO _layer;
		public int blendShapeIndex;
		public float offset;
	}

	#endregion

	#region Private Fields
	//Serialized
	[Tooltip("Layer To use for blendshape")]
	[SerializeField] private List<LayerBlendShape> _layerBlendShapes;
	[Tooltip("Value Updated event")]
	[SerializeField] private EventChannelSO _onValueUpdated;
	[Tooltip("Slider")]
	[SerializeField] private PinchSlider _slider;
	//Non-Serialized
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods
	#endregion Monobehavior Methods

	#region Private Methods
	#endregion Private Methods

	#region Public Methods
	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Callback for fat slider being updated
	/// </summary>
	/// <param name="eventData"></param>
	public void OnSliderUpdated(SliderEventData eventData)
	{
		foreach (LayerBlendShape layerBlendShape in _layerBlendShapes)
		{
			_onValueUpdated.RaiseEvent(layerBlendShape._layer, layerBlendShape.blendShapeIndex, Mathf.Clamp(eventData.NewValue + layerBlendShape.offset, 0f, 1f));
		}
	}

	/// <summary>
	/// Resets slider to 0
	/// </summary>
	public void ResetSlider()
	{
		_slider.SliderValue = 0f;
	}

	#endregion Events

}
