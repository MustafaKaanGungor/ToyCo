using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameEvents.InteractWithTribe += (Boy boy) => SetGameState(GameState.Selection);
        GameEvents.OnCharacterConfirmed += (CharacterSO character) => SetGameState(GameState.Map);

        SetGameState(GameState.Selection);
    }

    private void SetGameState(GameState newState)
    {
        CurrentState = newState;
        GameEvents.StateChanged?.Invoke(CurrentState);
    }
    
}

public enum GameState {
    Selection,
    Map,
    GameOver
}