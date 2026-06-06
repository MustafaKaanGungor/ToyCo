using UnityEngine;

public class WeddingPlace : MonoBehaviour, IMapInteractable
{
    public void OnInteracted(MapUnit initiator)
    {
        if(initiator is Sheep sheep)
        {
            sheep.DestroySheep();
        }
    }

    public void OnWithdraw(MapUnit initiator)
    {
        
    }
}
