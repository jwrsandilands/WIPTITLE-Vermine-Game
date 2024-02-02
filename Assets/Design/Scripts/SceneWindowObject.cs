using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWindowObject : MonoBehaviour
{
    public GameObject sceneCamera;
    public bool showPopupFlag;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (showPopupFlag)
        {
            gameObject.SetActive(true);
            sceneCamera.GetComponent<MoveCamera>().isSceneWindowActive = true;
        }
        else
        {
            gameObject.SetActive(false);
            sceneCamera.GetComponent<MoveCamera>().isSceneWindowActive = false;
            sceneCamera.GetComponent<MoveCamera>().isAnimationPlaying = false;
        }
    }
}
