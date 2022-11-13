using CrossComm.Framework.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyLayer : MonoBehaviour
{

	#region Private Fields
	//Serialized
	[SerializeField] private VeinsLayer _layer;
	//Non-Serialized
	private Renderer[] _renderers;
	private SkinnedMeshRenderer _blendShapeRenderer;
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods

	private void Start()
	{
		_renderers = GetComponentsInChildren<Renderer>();
		_blendShapeRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	#endregion Monobehavior Methods

	#region Private Methods

	/// <summary>
	/// Sets render state of all child renderers
	/// </summary>
	/// <param name="i_state"></param>
	private void SetRendererState(bool i_state)
	{
		foreach (Renderer renderer in _renderers)
		{
			renderer.enabled = i_state;
		}
	}

	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Callback for toggling layer
	/// </summary>
	/// <param name="i_args"></param>
	public void OnSetLayerState(CustomArgs i_args)
	{
		VeinsLayer receivedLayer = i_args.GetObject<VeinsLayer>(0);
		if (receivedLayer == _layer)
		{
			Debug.Log(_layer + " " + i_args.GetObject<bool>(1));
			SetRendererState(i_args.GetObject<bool>(1));
		}
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Callback for updating blendshape
	/// </summary>
	/// <param name="i_args"></param>
	public void OnBlendShapeUpdated(CustomArgs i_args)
	{
		if (i_args.GetObject<VeinsLayer>(0) != _layer)
		{
			return;
		}

		int blendshapeIndex = i_args.GetObject<int>(1);

		if (_blendShapeRenderer == null)
		{
			_blendShapeRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		}

		_blendShapeRenderer.SetBlendShapeWeight(blendshapeIndex, i_args.GetObject<float>(2) * 100);
	}

	#endregion Events

}
