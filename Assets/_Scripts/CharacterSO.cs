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
                    return "Hair: Hair 1";
                case HairStyle.hair2:
                    return "Hair: Hair 2";
                case HairStyle.hair3:
                    return "Hair: Hair 3";
            }
        }
        else if(style is EyeStyle)
        {
            switch((EyeStyle)(object)style)
            {
                case EyeStyle.eye1:
                    return "Eyes: Eyes 1";
                case EyeStyle.eye2:
                    return "Eyes: Eyes 2";
                case EyeStyle.eye3:
                    return "Eyes: Eyes 3";
            }
        }
        else if(style is MouthStyle)
        {
            switch((MouthStyle)(object)style)
            {
                case MouthStyle.mouth1:
                    return "Mouth: Mouth 1";
                case MouthStyle.mouth2:
                    return "Mouth: Mouth 2";
                case MouthStyle.mouth3:
                    return "Mouth: Mouth 3";
            }
        }
        else if(style is BodyStyle)
        {
            switch((BodyStyle)(object)style)
            {
                case BodyStyle.body1:
                    return "Body: Body 1";
                case BodyStyle.body2:
                    return "Body: Body 2";
                case BodyStyle.body3:
                    return "Body: Body 3";
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