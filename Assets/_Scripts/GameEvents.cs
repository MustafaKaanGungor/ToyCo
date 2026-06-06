using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Vector2> OnMoveInput;
    public static void TriggerMoveInput(Vector2 input) => OnMoveInput?.Invoke(input);
}
