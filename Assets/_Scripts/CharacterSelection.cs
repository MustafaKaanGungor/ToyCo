using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public List<CharacterSO> characterList;
    public GameObject characterPrefab;
    public List<Transform> characterPositions;
    public Button confirmButton;

    private List<CharacterDisplay> _characterDisplays = new();
    private int _selectedIndex = -1;

    private void Awake()
    {
        if (confirmButton != null)
            confirmButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        InitializeCharacterSelection();
    }

    public void InitializeCharacterSelection()
    {
        List<CharacterSO> characterSOs = new List<CharacterSO>(characterList);

        foreach (var position in characterPositions)
        {
            if (characterSOs.Count == 0)
                break;

            int randomIndex = Random.Range(0, characterSOs.Count);
            CharacterSO selectedCharacter = characterSOs[randomIndex];
            characterSOs.RemoveAt(randomIndex);

            var character = Instantiate(characterPrefab, position.position, Quaternion.identity, this.transform);
            var display = character.GetComponent<CharacterDisplay>();
            display.Initialize(selectedCharacter, this);
            _characterDisplays.Add(display);
        }
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

    public void ConfirmSelection()
    {
        if (_selectedIndex < 0)
            return;

        GameEvents.TriggerCharacterConfirmed(_characterDisplays[_selectedIndex].CharacterData);
        gameObject.SetActive(false);
    }
}
