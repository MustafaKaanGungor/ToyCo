using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Vector2> OnMoveInput;
    public static void TriggerMoveInput(Vector2 input) => OnMoveInput?.Invoke(input);

    public static event Action<CharacterSO> OnSelectionChanged;
    public static void TriggerSelectionChanged(CharacterSO character) => OnSelectionChanged?.Invoke(character);

    public static event Action<CharacterSO> OnCharacterConfirmed;
    public static void TriggerCharacterConfirmed(CharacterSO character) => OnCharacterConfirmed?.Invoke(character);
    public static  Action<Boy> InteractWithTribe;

    public static Action<GameState> StateChanged;

    public static event Action OnSelectionCancelled;
    public static void TriggerSelectionCancelled() => OnSelectionCancelled?.Invoke();
    public static Action<Boy> SpawnSheep;

    public static Action<Boy> SpawnWeddingPlace;
    public static Action SheepDelivered;
    public static Action OpenSlecetionPanel;
    //auido
    public static Action<SfxType> PlaySound;
    public static Action StopSfx;
    public static event Action<MusicType> PlayMusic;
    public static event Action<bool> StopMusic; // bool = fade?
    public static void TriggerPlaySound(SfxType type) => PlaySound?.Invoke(type);
    public static void TriggerStopSfx() => StopSfx?.Invoke();
    public static void TriggerPlayMusic(MusicType type) => PlayMusic?.Invoke(type);
    public static void TriggerStopMusic(bool fade = true) => StopMusic?.Invoke(fade);
}
                       