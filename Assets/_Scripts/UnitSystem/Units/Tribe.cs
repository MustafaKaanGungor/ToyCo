using System;
using UnityEngine;

public class Tribe : MapUnit
{
    [SerializeField] private Boy _tribeType;
    public Boy TribeType => _tribeType;
    private bool _isInteracted = false;
    [SerializeField] private GameObject _sheepPrefab;
    [SerializeField] private GameObject _weddingPlacePrefab;
    [SerializeField] private float _weddingPlaceMinDistance = 5f;
    private void OnEnable()
    {
        GameEvents.InteractWithTribe += OnInteractWithTribe;
        GameEvents.SpawnSheep += OnSpawnSheep;
        GameEvents.SpawnWeddingPlace += OnSpawnWeddingPlace;
    }


    private void OnDisable()
    {
        GameEvents.InteractWithTribe -= OnInteractWithTribe;
        GameEvents.SpawnSheep -= OnSpawnSheep;
        GameEvents.SpawnWeddingPlace -= OnSpawnWeddingPlace;
    }

    private void OnInteractWithTribe(Boy boy)
    {
        if (_isInteracted)
        {
            return;
        }
        if (boy == TribeType)
        {
            //Secim menusunu ac
            Debug.Log($"Interacted with tribe of type {TribeType}");

            _isInteracted = true;
        }
    }

    protected override void SelectNextTarget()
    {
        TargetPosition = GetRandomPositionInBounds();
    }
    public override void OnInteracted(MapUnit initiator)
    {
        if (initiator is Tribe otherTribe)
        {
            //HandleTribeDiplomacy(otherTribe);
        }
    }
    public override void OnWithdraw(MapUnit initiator)
    {
        //HandleTribeWithdrawal(initiator);
    }
    private void OnSpawnSheep(Boy boy)
    {
        if (TribeType == boy)
        {
            GameObject sheep = Instantiate(_sheepPrefab, transform.position, Quaternion.identity);
            sheep.GetComponent<Sheep>().InitializeSheep(25);
        }
    }
    private void OnSpawnWeddingPlace(Boy boy)
    {
        if (TribeType != boy)
        {
            return;
        }
        Vector2 spawnPos;
        int attempts = 0;
        do
        {
            spawnPos = GetRandomPositionInBounds();
            attempts++;
        }
        while (Vector2.Distance(spawnPos, transform.position) < _weddingPlaceMinDistance && attempts < 30);

        Instantiate(_weddingPlacePrefab, spawnPos, Quaternion.identity);
    }
}
