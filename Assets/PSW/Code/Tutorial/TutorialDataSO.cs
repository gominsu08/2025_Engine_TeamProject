using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "SO/Tutorial/Data")]
public class TutorialDataSO : ScriptableObject
{
    public Sprite tutorialImage;
    [TextArea]
    public string tutorialText;
}
