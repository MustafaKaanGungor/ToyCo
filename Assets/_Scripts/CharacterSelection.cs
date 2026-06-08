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
    public GameObject traitPanelPrefab;
    public List<Transform> characterPositions;
    public Button confirmButton;
    [SerializeField] private Button _backButton;
    public CinemachineCamera selectionCamera;

    [Header("Single Selection")]
    [SerializeField] private CharacterSO _singleSelectCharacter;

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

        if (_backButton != null)
            _backButton.gameObject.SetActive(false);

        if (searchedCharacter == null)
            InitializeSingleSelection();
        else
            InitializeMatchSelection();

        if (_backButton != null)
        {
            _backButton.gameObject.SetActive(true);
            _backButton.interactable = _currentPhase != SelectionPhase.SingleSelect;
        }

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

            if (_backButton != null)
            {
                _backButton.gameObject.SetActive(true);
                _backButton.interactable = _currentPhase != SelectionPhase.SingleSelect;
            }   
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
        searchedCharacter = characterList[Random.Range(0, characterList.Count)];
        SpawnCharacter(_singleSelectCharacter, characterPositions[0].position);
        SpawnTraitPanel(searchedCharacter);

    }

    private void InitializeMatchSelection()
    {
        _currentPhase = SelectionPhase.MatchSelect;

        

        List<CharacterSO> picked = new List<CharacterSO>(characterList);
        Shuffle(picked);

        for (int i = 0; i < picked.Count; i++) {
            SpawnCharacter(picked[i], GetCharacterPosition(i, picked.Count));
            SpawnTraitPanel(picked[i]);
        }
    }

    private Vector3 GetCharacterPosition(int index, int total)
    {
        if (characterPositions.Count == 0)
            return Vector3.zero;

        if (total <= characterPositions.Count && index < characterPositions.Count)
            return characterPositions[index].position;

        if (characterPositions.Count < 2)
            return characterPositions[0].position;

        Vector3 leftPos = characterPositions[0].position;
        Vector3 rightPos = characterPositions[characterPositions.Count - 1].position;
        float spacing = (rightPos.x - leftPos.x) / Mathf.Max(total - 1, 1);
        return leftPos + new Vector3(spacing * index, 0f, 0f);
    }

    private void SpawnCharacter(CharacterSO data, Vector3 position)
    {
        var character = Instantiate(characterPrefab, position, Quaternion.identity, transform);
        var display = character.GetComponent<CharacterDisplay>();
        display.Initialize(data, this);
        _characterDisplays.Add(display);
        _instantiatedObjects.Add(character);
    }

    private void SpawnTraitPanel(CharacterSO data)
    {
        var panel = Instantiate(traitPanelPrefab, transform);
        panel.GetComponent<TraitPanel>().SetTraits(data);
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

    public void CancelSelection()
    {
        FadeOutAndDeactivate();
        GameEvents.TriggerSelectionCancelled();
    }

    private void FadeOutAndDeactivate()
    {
        foreach (var display in _characterDisplays)
            display.FadeOut();

        if (confirmButton != null)
            confirmButton.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => confirmButton.transform.DOScale(1f, 0.01f));

        if (_backButton != null)
            _backButton.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => _backButton.transform.DOScale(1f, 0.01f));

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
