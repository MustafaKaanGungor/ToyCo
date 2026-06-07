using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class WeddingPlace : MonoBehaviour, IMapInteractable
{
    private void Awake()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = 1.5f;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnInteracted(MapUnit initiator)
    {
        if (initiator is Sheep sheep)
        {
            Vector2 distance = transform.position - sheep.transform.position;
            if (distance.magnitude <= 1.5f)
            {
                sheep.DestroySheep();
                GameEvents.SheepDelivered?.Invoke();
            }
        }
    }

    public void OnWithdraw(MapUnit initiator)
    {
    }
}
