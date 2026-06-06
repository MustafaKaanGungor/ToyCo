using UnityEngine;

public class Tribe : MapUnit
{
    protected override void SelectNextTarget()
    {
        TargetPosition = GetRandomPositionInBounds();
    }
}
