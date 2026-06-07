using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public Gender gender;
    public Boy boy; 
    public HairStyle hairStyle;
    public EyeStyle eyeStyle;
    public MouthStyle mouthStyle;
    public BodyStyle bodyStyle;

    public string GetStyleString<T>(T style)
    {
        if(style is HairStyle)
        {
            switch((HairStyle)(object)style)
            {
                case HairStyle.hair1:
                    return "Hafif Esmer";
                case HairStyle.hair2:
                    return "Esmer";
                case HairStyle.hair3:
                    return "Beyaz Teli";
            }
        }
        else if(style is EyeStyle)
        {
            switch((EyeStyle)(object)style)
            {
                case EyeStyle.eye1:
                    return "Mavi Gözlü";
                case EyeStyle.eye2:
                    return "Kısa Saçlı";
                case EyeStyle.eye3:
                    return "Uzun Saçlı";
            }
        }
        else if(style is MouthStyle)
        {
            switch((MouthStyle)(object)style)
            {
                case MouthStyle.mouth1:
                    return "Ağır Başlı";
                case MouthStyle.mouth2:
                    return "Güler Yüzlü";
                case MouthStyle.mouth3:
                    return "Sert Bakışlı";
            }
        }
        else if(style is BodyStyle)
        {
            switch((BodyStyle)(object)style)
            {
                case BodyStyle.body1:
                    return "Sakalsız";
                case BodyStyle.body2:
                    return "Bıyıklı";
                case BodyStyle.body3:
                    return "Soğukkanlı";
            }
        }

        return style.ToString();
    }
}

public enum Boy
{
    Boy1,
    Boy2,
    Boy3,
    Boy4,
    Boy5
}

public enum Gender
{
    Male,
    Female
}

public enum HairStyle
{
    hair1,
    hair2,
    hair3
}

public enum EyeStyle
{
    eye1,
    eye2,
    eye3
}

public enum MouthStyle
{
    mouth1,
    mouth2,
    mouth3
}

public enum BodyStyle
{
    body1,
    body2,
    body3

}