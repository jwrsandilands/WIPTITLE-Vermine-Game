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
    public Image dialogueTalkSprite;
    public CharacterManager characterManager;

    private Queue<PrintDialogue> printDialogues;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        printDialogues = new Queue<PrintDialogue>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerCamera.GetComponent<MoveCamera>().isSceneWindowActive = true;
        dialoguePanel.SetActive(true);

        printDialogues.Clear();

        foreach (PrintDialogue step in dialogue.printDialogues)
        {
            printDialogues.Enqueue(step);
        }

        DisplayNextStep();
    }

    public void DisplayNextStep()
    {
        if (printDialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        PrintDialogue stepDialogue = printDialogues.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeDialogue(stepDialogue));
    }

    IEnumerator TypeDialogue(PrintDialogue step)
    {
        dialogueText.text = "";
        foreach(Character character in characterManager.characters)
        {
            if(character.id == step.character)
            {
                speakerName.text = character.name;

                int count = 0;
                foreach (Sprite talkSprite in character.talkSprites)
                {
                    if (count == step.emotion)
                    {
                        dialogueTalkSprite.sprite = talkSprite;
                    }
                    count++;
                }
            }
        }

        foreach (char letter in step.dialogue.ToCharArray())
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
