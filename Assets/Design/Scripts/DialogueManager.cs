using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject playerCamera;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogueText;

    private Queue<string> steps;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        steps = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        speakerName.text = dialogue.name;

        playerCamera.GetComponent<MoveCamera>().isSceneWindowActive = true;
        dialoguePanel.SetActive(true);

        steps.Clear();

        foreach(string step in dialogue.stepDialogue)
        {
            steps.Enqueue(step);
        }

        DisplayNextStep();
    }

    public void DisplayNextStep()
    {
        if (steps.Count == 0)
        {
            EndDialogue();
            return;
        }

        string stepDialogue = steps.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeDialogue(stepDialogue));
    }

    IEnumerator TypeDialogue (string step)
    {
        dialogueText.text = "";
        foreach (char letter in step.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerCamera.GetComponent<MoveCamera>().isSceneWindowActive = false;
    }
}
