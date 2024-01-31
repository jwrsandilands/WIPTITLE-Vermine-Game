using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextExtractor
{
    public static Dialogue ExtractDialogue(TextAsset dialogueFile)
    {
        Dialogue dialogue = new();
        List<PrintDialogue> addPrintDialogue = new();

        string[] dialogueSteps = dialogueFile.text.Split('$');

        int characterIndex = 0, emotionIndex = 0;

        for (int i = 1; i < dialogueSteps.Length; i++)
        {
            if(dialogueSteps[0].ToCharArray()[0] == 'D')
            {
                try
                {
                    characterIndex = int.Parse(dialogueSteps[i]);
                    i++;
                    emotionIndex = int.Parse(dialogueSteps[i]);
                    i++;
                } 
                catch
                {
                    throw new InvalidCastException($"{dialogueFile.name}'s Dialogue Indecies Loaded Incorrectly!");
                }
            }
            addPrintDialogue.Add(new PrintDialogue(characterIndex, emotionIndex, dialogueSteps[i].Trim()));
        }

        dialogue.printDialogues = addPrintDialogue;

        return dialogue;
    }
}
