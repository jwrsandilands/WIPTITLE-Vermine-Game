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
    public TextFormatCodeManager textCodeManager;
    public AnimationCodeManager animationCodeManager;
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

        bool readingTextCode = false, readingAnimationCode = false;
        string readCode = "";
        foreach (char letter in step.dialogue.ToCharArray())
        {
            switch(letter)
            {
                case '[':
                    readingTextCode = true;
                    continue;
                case ']':
                    readingTextCode = false;
                    continue;
                case '{':
                    readingAnimationCode = true;
                    continue;
                case '}':
                    readingAnimationCode = false;
                    continue;
            };

            if (readCode.Length > 30)
            {
                readingTextCode = false;
                readCode = "";
            }

            if (readingTextCode)
            {
                readCode += letter;

                if(textCodeManager.formatCodes.Any(e => e.textCode == readCode))
                {
                    TextFormatCode applyCode = textCodeManager.formatCodes.Where(e => e.textCode == readCode).FirstOrDefault();
                    readCode = "";

                    SetDialogueAttributes(applyCode);
                }
            }
            else if (readingAnimationCode)
            {
                readCode += letter;

                if(animationCodeManager.animationCodes.Any(e => e.animationCode == readCode))
                {
                    AnimationCode animateCode = animationCodeManager.animationCodes.Where(e => e.animationCode == readCode).FirstOrDefault();
                    readCode = "";

                    spriteAnimator.PerformAnimation(animateCode.animationToPlay);
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
    }

    private void SetDialogueAttributes(TextFormatCode inputCode)
    {
        string tagStart = "<";
        
        if (inputCode.textCode == "d")
        {
            tagStart = "</";
        }
        dialogueText.text += $"{tagStart}color=#{ColorUtility.ToHtmlStringRGBA(inputCode.textColor)}>";

        if (inputCode.bold)
        {
            dialogueText.text = dialogueText.text + tagStart + "b>";
        }
        if (inputCode.italic)
        {
            dialogueText.text = dialogueText.text + tagStart + "i>";
        }
        if (inputCode.underline)
        {
            dialogueText.text = dialogueText.text + tagStart + "u>";
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerCamera.GetComponent<MoveCamera>().isSceneWindowActive = false;
    }
}
