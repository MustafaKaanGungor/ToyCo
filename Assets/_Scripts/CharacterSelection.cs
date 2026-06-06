using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Cinemachine;

public class CharacterSelection : MonoBehaviour
{
    public CharacterSO searchedCharacter;
    public List<CharacterSO> characterList;
    public GameObject characterPrefab;
    public List<Transform> characterPositions;
    public Button confirmButton;
    public CinemachineCamera selectionCamera;

    private enum SelectionPhase { SingleSelect, MatchSelect }
    private SelectionPhase _currentPhase;

    private List<CharacterDisplay> _characterDisplays = new();
    private List<GameObject> _instantiatedObjects = new();
    private int _selectedIndex = -1;

    private void OnEnable()
    {
        ClearDisplays();

        if (confirmButton != null)
            confirmButton.gameObject.SetActive(false);

        if (searchedCharacter == null)
            InitializeSingleSelection();
        else
            InitializeMatchSelection();

        GameEvents.StateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.Selection)
        {
            ClearDisplays();
            selectionCamera.Priority = 10;
            if (searchedCharacter == null)
                InitializeSingleSelection();
            else
                InitializeMatchSelection();   
        } else if (state == GameState.Map)
        {
            ClearDisplays();
            selectionCamera.Priority = 0;
        }
    }


    private void OnDestroy()
    {
        ClearDisplays();

        GameEvents.StateChanged -= OnGameStateChanged;
    }

    private void ClearDisplays()
    {
        foreach (var obj in _instantiatedObjects)
            Destroy(obj);
        _instantiatedObjects.Clear();
        _characterDisplays.Clear();
        _selectedIndex = -1;
    }

    private void InitializeSingleSelection()
    {
        _currentPhase = SelectionPhase.SingleSelect;
        CharacterSO chosen = characterList[Random.Range(0, characterList.Count)];
        SpawnCharacter(chosen, characterPositions[0]);
    }

    private void InitializeMatchSelection()
    {
        _currentPhase = SelectionPhase.MatchSelect;

        List<CharacterSO> pool = new List<CharacterSO>(characterList);
        pool.Remove(searchedCharacter);

        List<CharacterSO> picked = new List<CharacterSO> { searchedCharacter };
        for (int i = 0; i < 2 && pool.Count > 0; i++)
        {
            int idx = Random.Range(0, pool.Count);
            picked.Add(pool[idx]);
            pool.RemoveAt(idx);
        }

        Shuffle(picked);

        for (int i = 0; i < picked.Count; i++)
            SpawnCharacter(picked[i], characterPositions[i]);
    }

    private void SpawnCharacter(CharacterSO data, Transform position)
    {
        var character = Instantiate(characterPrefab, position.position, Quaternion.identity, transform);
        var display = character.GetComponent<CharacterDisplay>();
        display.Initialize(data, this);
        _characterDisplays.Add(display);
        _instantiatedObjects.Add(character);
    }

    public void OnCharacterClicked(CharacterDisplay display)
    {
        int index = _characterDisplays.IndexOf(display);
        if (index < 0 || index == _selectedIndex)
            return;

        if (_selectedIndex >= 0)
            _characterDisplays[_selectedIndex].Deselect();

        _selectedIndex = index;
        _characterDisplays[_selectedIndex].Select();

        if (confirmButton != null)
            confirmButton.gameObject.SetActive(true);

        GameEvents.TriggerSelectionChanged(_characterDisplays[_selectedIndex].CharacterData);
    }

    public bool ConfirmSelection()
    {
        if (_selectedIndex < 0)
            return false;

        if (_currentPhase == SelectionPhase.MatchSelect
            && _characterDisplays[_selectedIndex].CharacterData != searchedCharacter)
            return false;

        if (_currentPhase == SelectionPhase.SingleSelect)
            searchedCharacter = _characterDisplays[_selectedIndex].CharacterData;

        FadeOutAndDeactivate();
        GameEvents.TriggerCharacterConfirmed(_characterDisplays[_selectedIndex].CharacterData);
        return true;
    }

    private void FadeOutAndDeactivate()
    {
        foreach (var display in _characterDisplays)
            display.FadeOut();

        if (confirmButton != null)
            confirmButton.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => confirmButton.transform.DOScale(1f, 0.01f));

    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
