using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrintDialogue
{
    public PrintDialogue(int character, int emotion, string dialogue)
    {
        this.character = character;
        this.emotion = emotion;
        this.dialogue = dialogue;
    }

    public int character, emotion;

    [TextArea(1, 2)]
    public string dialogue;
}
