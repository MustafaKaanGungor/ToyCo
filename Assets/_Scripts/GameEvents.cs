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
}
