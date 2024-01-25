using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject gameCamera, clickDisabler;
    public float turnSpeed;
    public bool isSceneWindowActive, isAnimationPlaying;

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        //disable clicking
        clickDisabler.SetActive(isAnimationPlaying || isSceneWindowActive);

        //handle movement
        if (!isSceneWindowActive)
        {
            if (Input.GetKey(KeyCode.A))
            {
                gameCamera.transform.Rotate(new Vector3(0, -turnSpeed, 0));
            }

            if (Input.GetKey(KeyCode.D))
            {
                gameCamera.transform.Rotate(new Vector3(0, turnSpeed, 0));
            }
        }
    }
}
