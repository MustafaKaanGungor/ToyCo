using System;
using UnityEngine;

public class Tribe : MapUnit
{
    [SerializeField] private Boy _tribeType;
    public Boy TribeType => _tribeType;
    private bool _isInteracted = false;
    private void OnEnable()
    {
        GameEvents.InteractWithTribe += OnInteractWithTribe;
        //GameEvents.CharachterConfirmed += OnSpawnSheep;
    }
    private void OnDisable()
    {
        GameEvents.InteractWithTribe -= OnInteractWithTribe;
        //GameEvents.CharachterConfirmed -= OnSpawnSheep;

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
    private void OnSpawnSheep()
    {

    }
}
