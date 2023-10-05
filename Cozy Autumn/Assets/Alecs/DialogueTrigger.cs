using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public TMP_Text dialogueText;
    public TMP_Text characterName;
    public GameObject canvasDialogue;
    public float typewriterSpeed = 0.05f;

    private int ind = 0;
    private int startingInd = -1;
    private bool isInRange = false;
    private bool isWriting = false;
    private bool conversationEnded = true;

    Coroutine co;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            startingInd--;
            ind--;
            canvasDialogue.gameObject.SetActive(false);
            //EndConversation();
        }
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyUp(KeyCode.F) && !isWriting)
        {
            if (conversationEnded)
            {
                ind = startingInd;
                StartConversation();
            }
            else
            {
                DisplayNextSentence();
            }
        }
        else if (isInRange && Input.GetKeyUp(KeyCode.F) && isWriting)
        {
            StopCoroutine(co);
            dialogueText.text = dialogue.sentences[--ind];
            isWriting = false;
            ind++;
            startingInd = ind;
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isWriting = true;
        foreach (char letter in sentence)
        {
            if (isWriting)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typewriterSpeed);
            }
            else
            {
                break;
            }
        }
        isWriting = false;
    }

    private void StartConversation()
    {
        // if (dialogue.sentences.Length > 0)
        //{
        ind = startingInd + 1;
        conversationEnded = false;
        DisplayNextSentence();
        //  }
    }

    private void DisplayNextSentence()
    {
        if (dialogue.sentences[ind] == string.Empty)
        {
            startingInd = ind;
            EndConversation();
        }
        else if (ind < dialogue.sentences.Length)
        {
            Debug.Log("Displaying dialogue: " + dialogue.sentences[ind]);
            canvasDialogue.gameObject.SetActive(true);
            characterName.text = dialogue.characterName;

            co = StartCoroutine(TypeSentence(dialogue.sentences[ind]));
            ind++;
        }
        else
        {
            EndConversation();
        }
    }

    private void EndConversation()
    {
        canvasDialogue.gameObject.SetActive(false);
        conversationEnded = true;
    }
}
