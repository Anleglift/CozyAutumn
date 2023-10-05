using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Character Info")]
    [TextArea(1, 10)]
    public string characterName;

    [Header("Dialogue Text")]
    [TextArea(3, 10)]
    public string[] sentences;

    public TMP_Text dialogueText;

}
