using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogs : ScriptableObject
{
    [Tooltip("Character speaking")] public Characters Character;
    [TextArea(10,10)] public string Dialogue;
    [Tooltip("Next dialogue")] public Dialogs NextDialogue;

    public void UpdateDialog(TextMeshProUGUI textComponentName, TextMeshProUGUI textComponent)
    {
        textComponentName.text = Character.Name;
        textComponent.text = Dialogue;
    }
}
