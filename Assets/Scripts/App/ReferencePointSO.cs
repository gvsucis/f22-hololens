using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reference Point")]
public class ReferencePointSO : ScriptableObject
{

	#region Public Fields
	[Tooltip("Name of reference point")]
	public string referencePointName;
	[Tooltip("Y indices on lattice to control")]
	public List<int> yIndicesToControl;
	#endregion Public Fields
}
