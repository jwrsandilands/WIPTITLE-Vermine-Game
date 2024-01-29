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
        List<string> addDialogue = new();

        string[] dialogueSteps = dialogueFile.text.Split('$');

        int characterIndex, emotionIndex;

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

            addDialogue.Add(dialogueSteps[i].Trim());
        }

        dialogue.stepDialogue = addDialogue;

        return dialogue;
    }
}
