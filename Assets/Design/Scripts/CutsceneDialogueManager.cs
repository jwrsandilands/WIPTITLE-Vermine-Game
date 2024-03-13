using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject playerCamera;
    public TextMeshProUGUI dialogueText;
    public TextFormatCodeManager textCodeManager;
    public SoundEffectCodeManager sfxCodeManager;
    public AudioSource sfxPlayer;

    private Queue<PrintDialogue> printDialogues = new Queue<PrintDialogue>();

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

        bool readingTextCode = false, readingSfxCode = false;
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
                case '<':
                    readingSfxCode = true;
                    continue;
                case '>':
                    readingSfxCode = false;
                    continue;
            };

            if (readCode.Length > 30)
            {
                readingTextCode = false;
                readingSfxCode = false;
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

                    if(applyCode.soundEffect != null)
                    {
                        sfxPlayer.clip = applyCode.soundEffect;
                        sfxPlayer.Play();
                    }
                }
            }
            else if (readingSfxCode)
            {
                readCode += letter;

                if(sfxCodeManager.sfxCodes.Any(e => e.sfxCode == readCode))
                {
                    SoundEffectCode soundEffectCode = sfxCodeManager.sfxCodes.Where(e => e.sfxCode == readCode).FirstOrDefault();
                    readCode = "";

                    sfxPlayer.clip = soundEffectCode.sfxToPlay;
                    sfxPlayer.Play();
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

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerCamera.GetComponent<MoveCamera>().isSceneWindowActive = false;
    }
}
