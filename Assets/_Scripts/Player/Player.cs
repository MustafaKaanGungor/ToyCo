using UnityEngine;

public class Player : MonoBehaviour , IMapInteractable
{

    public void OnInteracted(MapUnit initiator)
    {
        if (initiator is Tribe tribe)
        {
            GameEvents.InteractWithTribe?.Invoke(tribe.TribeType);
        }
    }
    public void OnWithdraw(MapUnit initiator)
    {
    }
}
