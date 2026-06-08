using TMPro;
using UnityEngine;

public class TraitPanel : MonoBehaviour
{
    public TMP_Text traitText1;
    public TMP_Text traitText2;
    public TMP_Text traitText3;
    public TMP_Text traitText4;

    public void SetTraits(CharacterSO character)
    {
        traitText1.text = character.GetStyleString(character.hairStyle);
        traitText2.text = character.GetStyleString(character.eyeStyle);
        traitText3.text = character.GetStyleString(character.mouthStyle);
        traitText4.text = character.GetStyleString(character.bodyStyle);
    }
}
