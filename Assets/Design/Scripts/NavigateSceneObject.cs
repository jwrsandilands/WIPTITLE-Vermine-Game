using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateSceneObject : MonoBehaviour
{
    public GameObject playerCamera;
    public Vector3 moveAmount;

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
            playerCamera.transform.position = moveAmount;
        }
    }
}
