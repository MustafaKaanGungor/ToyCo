using UnityEngine;

public class WeddingPlace : MonoBehaviour, IMapInteractable
{
    public void OnInteracted(MapUnit initiator)
    {
        if(initiator is Sheep sheep)
        {
            sheep.DestroySheep();
            GameEvents.SheepDelivered?.Invoke();
        }
    }

    public void OnWithdraw(MapUnit initiator)
    {
        
    }
}
