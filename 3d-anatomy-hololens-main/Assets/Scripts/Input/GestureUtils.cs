using Microsoft.MixedReality.Toolkit.Utilities;

public static class GestureUtils
{
    private const float PinchThreshold = 0.7f;

    public static bool IsPinching(Handedness trackedHand)
    {
        return HandPoseUtils.CalculateIndexPinch(trackedHand) > PinchThreshold;
    }
}
