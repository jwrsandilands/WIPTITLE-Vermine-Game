using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimatePickup : MonoBehaviour
{
    public Camera playerCamera;
    public Image popupScene;
    public GameObject UIManager;
    private float toTimeSpent, fromTimeSpent;
    public float toDuration, fromDuration;
    public float offset;

    private void Start()
    {
        UIManager = GameObject.Find("GlobalUI");
        gameObject.transform.SetParent(UIManager.transform);
    }

    void FixedUpdate()
    {
        if (toTimeSpent < toDuration)
        {
            playerCamera.GetComponent<MoveCamera>().isAnimationPlaying = true;

            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(popupScene.rectTransform.rect.width / 2, popupScene.rectTransform.rect.height / 2), toTimeSpent / toDuration);
            toTimeSpent += Time.deltaTime;
        }
        else if(fromTimeSpent < fromDuration)
        {
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(popupScene.rectTransform.rect.width - popupScene.rectTransform.rect.width * 1.2f, popupScene.rectTransform.rect.height * 1.2f), fromTimeSpent / fromDuration);
            fromTimeSpent += Time.deltaTime;
        }
        else
        {
            playerCamera.GetComponent<MoveCamera>().isAnimationPlaying = false;

            Destroy(gameObject);
        }
    }
}
