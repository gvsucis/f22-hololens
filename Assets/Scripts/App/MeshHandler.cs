using CrossComm.Framework.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHandler : MonoBehaviour
{

	#region Private Fields
	//Serialized
	[Tooltip("Gameobjects for Mesh")]
	[SerializeField] private BodyLayer[] _bodyLayers;
	[Tooltip("Reference points used in this mesh")]
	[SerializeField] private List<ReferencePoint> _referencePoints;
	[Tooltip("Transform anchor for head")]
	[SerializeField] private TransformAnchorSO _headAnchor;
	//Non-Serialized
	private Vector3 _firstPointOffset;
	private float _currenYScale;
	private float _currentYDistanceForReferences;
	private float _scaleToDistanceRatio;
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods

	private void Start()
	{
		SetUpMesh();
	}

	#endregion Monobehavior Methods

	#region Private Methods

	/// <summary>
	/// Sets up mesh points and scale
	/// </summary>
	private void SetUpMesh()
	{
		//Set inital offset
		_firstPointOffset = this.transform.position - _referencePoints[0].transform.position;

		//Send list of reference points in an event
	}

	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Sets position of mesh based on location of first point
	/// </summary>
	/// <param name="i_desiredPosition"></param>
	public void SetMeshPosition(Vector3 i_desiredPosition)
	{
		Vector3 lookDirection = (_headAnchor.value.position - i_desiredPosition).normalized;
		lookDirection.y = 0;
		this.transform.rotation = Quaternion.LookRotation(lookDirection);

		_firstPointOffset = this.transform.position - _referencePoints[0].transform.position;
		Vector3 offsettedPosition = i_desiredPosition + _firstPointOffset;
		this.transform.position = offsettedPosition;
	}

	/// <summary>
	/// Sets state of mesh gameobjects
	/// </summary>
	/// <param name="i_state"></param>
	public void SetMeshState(bool i_state)
	{
		foreach (BodyLayer layer in _bodyLayers)
		{
			layer.gameObject.SetActive(i_state);
		}
	}

	/// <summary>
	/// Returns if index requested for reference point is available
	/// </summary>
	/// <param name="i_index"></param>
	/// <returns></returns>

	public bool DoesReferencePointExist(int i_index)
	{
		if (i_index < _referencePoints.Count)
		{
			return true;
		}
		return false;
	}

	/// <summary>
	/// Returns reference point at given index
	/// </summary>
	/// <param name="i_index"></param>
	/// <returns></returns>
	public ReferencePoint GetReferencePoint(int i_index)
	{
		return _referencePoints[i_index];
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Toggles all reference points
	/// </summary>
	public void ToggleReferencePoints()
	{
		foreach (ReferencePoint point in _referencePoints)
		{
			point.ToggleReferencePointState();
		}
	}

	/// <summary>
	/// Callback for fat value updated event
	/// </summary>
	/// <param name="i_args"></param>
	public void OnFatValueUpdated(CustomArgs i_args)
	{

	}

	#endregion Events

}
