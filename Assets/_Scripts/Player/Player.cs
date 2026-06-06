using System;
using UnityEngine;

public class Player : MonoBehaviour , IMapInteractable
{
    private Rigidbody2D _rb;
    private Collider2D _collider;


    private void OnEnable()
    {
        GameEvents.StateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameEvents.StateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.Map)
        {
            _collider.enabled = true;
            _rb.simulated = true;
        } else
        {
            _collider.enabled = false;
            _rb.simulated = false;
        }
    }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }
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
