using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStartObject : MonoBehaviour
{
    public Sprite[] AnimationPanels;
    public GameObject AnimationPanel;
    public TextAsset DialogueFile;
    private Dialogue DialogueToPrint;

    private void Start()
    {
        DialogueToPrint = TextExtractor.ExtractDialogue(DialogueFile);
    }

    private void OnMouseEnter()
    {
        //gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    private void OnMouseExit()
    {
        //gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(gameObject.name);
            AnimationPanel.SetActive(true);
            AnimationPanel.GetComponent<CutsceneAnimation>().StartCutscene(AnimationPanels, DialogueToPrint);
        }
    }
}
