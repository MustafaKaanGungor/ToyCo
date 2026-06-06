using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public List<CharacterSO> characterList;
    public GameObject characterPrefab;
    public List<Transform> characterPositions;
    
    public void InitializeCharacterSelection()
    {
        List<CharacterSO> characterSOs = new List<CharacterSO>(characterList);
        foreach(var position in characterPositions)
        {
            int randomIndex = Random.Range(0, characterSOs.Count);
            CharacterSO selectedCharacter = characterSOs[randomIndex];
            var character = Instantiate(characterPrefab, position.position, Quaternion.identity);
            
        }
    }
}
