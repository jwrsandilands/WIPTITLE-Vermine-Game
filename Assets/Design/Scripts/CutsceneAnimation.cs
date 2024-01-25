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

    private void Start()
    {
        Button btn = NextButton.GetComponent<Button>();
        btn.onClick.AddListener(NextScene);
    }

    public void StartCutscene(Sprite[] givenFrames)
    {
        PlayerCamera.GetComponent<MoveCamera>().isAnimationPlaying = true;
        Frames = givenFrames;
        FrameIndex = 0;
        NextScene();
    }

    private void NextScene()
    {
        if (FrameIndex < Frames.Length)
        {
            CutsceneImage.sprite = Frames[FrameIndex];
            FrameIndex++;
        }
        else
        {
            PlayerCamera.GetComponent<MoveCamera>().isAnimationPlaying = false;
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
