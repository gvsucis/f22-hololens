using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Gestures/Pinch")]
public class PinchGesture : GestureAction
{
	public const float PinchThreshold = 0.7f;
	private Transform _transformValue;

	public override bool GetGesture()
	{
		if (HandJointService.IsHandTracked(_trackedHand) && HandPoseUtils.CalculateIndexPinch(_trackedHand) > PinchThreshold)
		{
			_transformValue = HandJointService.RequestJointTransform(TrackedHandJoint.IndexTip, _trackedHand);
			return true;
		}

		return false;
	}

	public override object GetParams()
	{
		return _transformValue;
	}
}
