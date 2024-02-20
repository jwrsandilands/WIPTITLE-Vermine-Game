using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public TextFormatCodeManager TextCodeManager;
    public SpriteAnimator spriteAnimator;

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

        bool readingCode = false;
        string readTextCode = "";
        TextFormatCode defaultCode = TextCodeManager.formatCodes.Where(e => e.textCode == "d").FirstOrDefault();
        TextFormatCode applyCode = new();
        foreach (char letter in step.dialogue.ToCharArray())
        {
            if(letter == '[')
            {
                readingCode = true;
                continue;
            }
            else if(letter == ']')
            {
                readingCode = false;
                readTextCode = "";
                continue;
            }

            if (readingCode)
            {
                readTextCode += letter;

                if(readTextCode == "d")
                {
                    dialogueText.text += $"<color=#{ColorUtility.ToHtmlStringRGBA(defaultCode.textColor)}>";

                    if (applyCode.bold)
                    {
                        dialogueText.text += "</b>";
                    }
                    if (applyCode.italic)
                    {
                        dialogueText.text += "</i>";
                    }
                    if (applyCode.underline)
                    {
                        dialogueText.text += "</u>";
                    }

                    continue;
                }
                else if(TextCodeManager.formatCodes.Any(e => e.textCode == readTextCode))
                {
                    applyCode = TextCodeManager.formatCodes.Where(e => e.textCode == readTextCode).FirstOrDefault();

                    // MOVE LATER
                    spriteAnimator.PerformAnimation(SpriteAnimationEnum.Hop);
                    //

                    dialogueText.text += $"<color=#{ColorUtility.ToHtmlStringRGBA(applyCode.textColor)}>";
                    if (applyCode.bold)
                    {
                        dialogueText.text += "<b>";
                    }
                    if (applyCode.italic)
                    {
                        dialogueText.text += "<i>";
                    }
                    if (applyCode.underline)
                    {
                        dialogueText.text += "<u>";
                    }
                    continue;
                }
                else
                {
                    if (readTextCode.Length > 30)
                    {
                        readingCode = false;
                        readTextCode = "";
                    }
                    continue;
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerCamera.GetComponent<MoveCamera>().isSceneWindowActive = false;
    }
}
