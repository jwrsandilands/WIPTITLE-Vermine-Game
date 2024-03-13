using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneAnimation : MonoBehaviour
{
    private Sprite[] Frames;
    private int FrameIndex = 0;
    public Image CutsceneImage;
    public Button NextButton;
    public GameObject PlayerCamera;
    public CutsceneDialogueManager DialogueManager;
    private Dialogue DialogueToPrint;

    private void Start()
    {
        Button btn = NextButton.GetComponent<Button>();
        btn.onClick.AddListener(NextScene);
    }

    public void StartCutscene(Sprite[] givenFrames, Dialogue dialogue)
    {
        gameObject.SetActive(true);
        PlayerCamera.GetComponent<MoveCamera>().isAnimationPlaying = true;
        Frames = givenFrames;
        FrameIndex = 0;
        DialogueToPrint = dialogue;

        NextScene();
    }

    private void NextScene()
    {
        if (FrameIndex < Frames.Length)
        {
            if(FrameIndex == 0)
            {
                DialogueManager.StartDialogue(DialogueToPrint);
            }
            else
            {
                DialogueManager.DisplayNextStep();
            }

            CutsceneImage.sprite = Frames[FrameIndex];
            FrameIndex++;
        }
        else
        {
            PlayerCamera.GetComponent<MoveCamera>().isAnimationPlaying = false;
            DialogueManager.EndDialogue();
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }
}
