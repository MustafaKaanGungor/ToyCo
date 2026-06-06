using UnityEngine;

public class Wolf : MapUnit
{
    protected override void SelectNextTarget()
    {
        TargetPosition = GetRandomPositionInBounds();
    }
}
