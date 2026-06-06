using UnityEngine;

public interface IMapInteractable
{
    void OnInteracted(MapUnit initiator);
    void OnWithdraw(MapUnit initiator);
}
