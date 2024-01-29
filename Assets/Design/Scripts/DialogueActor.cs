using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActor : MonoBehaviour
{
    public Dialogue dialogue;

    public TextAsset dialogueFile;

    private void Start()
    {
        dialogue = TextExtractor.ExtractDialogue(dialogueFile);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
