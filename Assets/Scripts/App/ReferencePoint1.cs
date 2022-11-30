using Deform;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ReferencePoint : MonoBehaviour
{
	#region Data

	[Serializable]
	public class ControlPoint
	{
		public enum ControlPointType
		{
			Default,
			Centered, // Use only y and z offsets 
			MirroredX, //Mirror x Axis location
		}


		[Tooltip("Type of control point")]
		[SerializeField] private ControlPointType _controlPointType;
		[Tooltip("Index of 3d lattice position")]
		[SerializeField] private Vector3Int _controlPointIndex;
		[SerializeField] private Vector3 _initialPosition;
		[SerializeField] private Vector3 _offset;
		private Transform _referencePoint;
		private LatticeDeformer _latticeDeformer;
		/// <summary>
		/// Sets up control point initial position and offset
		/// </summary>
		/// <param name="i_latticeDeformer"></param>
		/// <param name="i_referencePoint"></param>
		public void SetUp(LatticeDeformer i_latticeDeformer, Transform i_referencePoint, Vector3Int i_controlPointIndex)
		{
			_referencePoint = i_referencePoint;
			_latticeDeformer = i_latticeDeformer;
			_controlPointIndex = i_controlPointIndex;

			_initialPosition = i_latticeDeformer.GetControlPoint(_controlPointIndex.x, _controlPointIndex.y, _controlPointIndex.z);

			if (_initialPosition.x < 0)
			{
				_controlPointType = ControlPointType.Default;
			}
			else if (_initialPosition.x > 0)
			{
				_controlPointType = ControlPointType.MirroredX;
			}
			else if (_initialPosition.x == 0)
			{
				_controlPointType = ControlPointType.Centered;
			}

			switch (_controlPointType)
			{
				case ControlPointType.Default:
					_offset = _initialPosition - _referencePoint.localPosition;
					break;
				case ControlPointType.Centered:
					_offset = _initialPosition - new Vector3(_initialPosition.x, _referencePoint.localPosition.y, _initialPosition.z);
					break;
				case ControlPointType.MirroredX:
					//Reverse x position of the reference point to mirror the positioning
					_offset = _initialPosition - new Vector3(-_referencePoint.localPosition.x, _referencePoint.localPosition.y, _referencePoint.localPosition.z);
					break;
			}
		}

		public void Update()
		{
			Vector3 newPosition = Vector3.zero;

			switch (_controlPointType)
			{
				case ControlPointType.Default:
					newPosition = (_referencePoint.localPosition + _offset);
					break;
				case ControlPointType.Centered:
					//Use only y position
					newPosition = new Vector3(_initialPosition.x, _referencePoint.localPosition.y, _initialPosition.z) + _offset;
					break;
				case ControlPointType.MirroredX:
					//Reverse x position of the reference point to mirror the positioning
					newPosition = new Vector3(-_referencePoint.localPosition.x, _referencePoint.localPosition.y, _referencePoint.localPosition.z) + _offset;
					break;
			}

			_latticeDeformer.SetControlPoint(_controlPointIndex.x, _controlPointIndex.y, _controlPointIndex.z, newPosition);
		}
	}

	#endregion Data

	#region Private Fields
	//Serialized

	[Tooltip("Reference point properties")]
	[SerializeField] private ReferencePointSO _referencePointSO;
	[Tooltip("Point Mesh")]
	[SerializeField] private GameObject _pointMesh;
	//Non-Serialized
	private List<ControlPoint> _linkedControlPoints;
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods

	private void Start()
	{
		SetUpControlPoints();
	}

	private void Update()
	{
		UpdateControlPoints();
	}

	#endregion Monobehavior Methods

	#region Private Methods

	/// <summary>
	/// Sets all linked controlpoints
	/// </summary>
	private void SetUpControlPoints()
	{
		_linkedControlPoints = new List<ControlPoint>();
		_pointMesh.SetActive(false);
		LatticeDeformer latticeDeformer = FindObjectOfType<LatticeDeformer>();
		foreach (int y in _referencePointSO.yIndicesToControl)
		{
			for (int x = 0; x < latticeDeformer.Resolution.x; x++)
			{
				for (int z = 0; z < latticeDeformer.Resolution.z; z++)
				{
					ControlPoint newPoint = new ControlPoint();
					newPoint.SetUp(latticeDeformer, this.transform, new Vector3Int(x, y, z));
					_linkedControlPoints.Add(newPoint);
				}
			}
		}
	}

	/// <summary>
	/// Updates control points with offset
	/// </summary>
	private void UpdateControlPoints()
	{
		foreach (ControlPoint controlPoint in _linkedControlPoints)
		{
			controlPoint.Update();
		}
	}

	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Returns name of reference point
	/// </summary>
	/// <returns></returns>
	public string GetName()
	{
		return _referencePointSO.referencePointName;
	}

	/// <summary>
	/// Sets state of current reference point
	/// </summary>
	/// <param name="i_state"></param>
	public void SetReferencePointState(bool i_state)
	{
		_pointMesh.SetActive(i_state);
	}

	/// <summary>
	/// Sets state of current reference point
	/// </summary>
	/// <param name="i_state"></param>
	public void ToggleReferencePointState()
	{
		_pointMesh.SetActive(!_pointMesh.activeInHierarchy);
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events
	#endregion Events

}
