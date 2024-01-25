using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject TextElement;
    public GameObject cameraFocusObject;
    public GameObject cameraFocusPoint, cameraAwayPoint;
    public bool isSceneWindowActive;

    private void OnMouseEnter()
	{
		gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
	}

    private void OnMouseExit()
    {
		gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(gameObject.name);
            TextElement.GetComponent<TextMeshProUGUI>().text = $"{gameObject.name} Collected!";
            TextElement.GetComponent<FadeTextAway>().notificationTrigger = true;
            TextElement.GetComponent<TextMeshProUGUI>().color = Color.white;

            GameObject created = Instantiate(cameraFocusObject, gameObject.transform.position, gameObject.transform.rotation);
            created.GetComponent<AnimatePickup>().focusPoint = cameraFocusPoint;
            created.GetComponent<AnimatePickup>().awayPoint = cameraAwayPoint;

            gameObject.SetActive(false);
        }
    }
}
