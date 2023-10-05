using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]

public class DialogueManager : MonoBehaviour
{
    [Header("Character Info")]
    public string characterName;

    [Header("Dialogue Text")]
    [TextArea(3, 10)]
    public string[] sentences;


    public GameObject canvasDialogue;
    public TMP_Text dialogueText;
    private void Start()
    {
        canvasDialogue.gameObject.SetActive(false);
    }
}
