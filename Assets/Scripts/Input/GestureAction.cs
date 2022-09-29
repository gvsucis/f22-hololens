using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;


public abstract class GestureAction : ScriptableObject
{
	[Tooltip("Which hand to track this gesture on")]
	[SerializeField] protected Handedness _trackedHand;

	protected IMixedRealityHandJointService handJointService;
	protected IMixedRealityHandJointService HandJointService =>
		handJointService ??
		(handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>());

	public abstract bool GetGesture();

	public abstract object GetParams();
}
