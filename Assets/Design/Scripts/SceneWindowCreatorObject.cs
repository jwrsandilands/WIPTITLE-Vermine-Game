using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWindowCreatorObject : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject sceneWindow;

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
            sceneWindow.SetActive(true);
            sceneWindow.GetComponent<SceneWindowObject>().showPopupFlag = true;
        }
    }
}
