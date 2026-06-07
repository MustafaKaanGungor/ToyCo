using System;
using UnityEngine;

public class Player : MonoBehaviour, IMapInteractable
{
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private bool didSheepTaken = false;
    [SerializeField] private Canvas _endCanvas;

    private void OnEnable()
    {
        GameEvents.StateChanged += OnGameStateChanged;
        GameEvents.SpawnSheep += OnSpawnSheep;
        GameEvents.SheepDelivered += OnSheepDelivered;
    }

    private void OnSheepDelivered()
    {
        didSheepTaken = false;
        _endCanvas.gameObject.SetActive(true);
    }

    private void OnSpawnSheep(Boy boy)
    {
        didSheepTaken = true;
    }

    private void OnDisable()
    {
        GameEvents.StateChanged -= OnGameStateChanged;
        GameEvents.SpawnSheep -= OnSpawnSheep;
        GameEvents.SheepDelivered -= OnSheepDelivered;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Map)
        {
            _collider.enabled = true;
            _rb.simulated = true;
        }
        else
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
        if (didSheepTaken)
        {
            return;
        }
        
        if (initiator is Tribe tribe)
        {
            GameEvents.InteractWithTribe?.Invoke(tribe.TribeType);
            GameEvents.SpawnSheep?.Invoke(tribe.TribeType);
            GameEvents.SpawnWeddingPlace?.Invoke(tribe.TribeType);
        }
    }
    public void OnWithdraw(MapUnit initiator)
    {
    }
}
