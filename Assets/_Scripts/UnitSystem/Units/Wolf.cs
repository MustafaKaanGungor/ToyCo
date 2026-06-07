using UnityEngine;

public class Wolf : MapUnit
{
    protected override void SelectNextTarget()
    {
        TargetPosition = GetRandomPositionInBounds();
    }
    public override void OnInteracted(MapUnit initiator)
    {
        if (initiator is Sheep sheep)
        {
            //player.TakeDamage(10);
            //Debug.Log("Wolf attacked the player!");
        }
    }
}
