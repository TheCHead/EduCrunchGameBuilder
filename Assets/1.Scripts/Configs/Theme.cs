using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "ScriptableObjects/CreateThemeConfigFile", order = 2)]
public class Theme : ScriptableObject
{
    public AnswerOptionGroup optionGroupPrefab;
    public Sprite counterSprite;
}