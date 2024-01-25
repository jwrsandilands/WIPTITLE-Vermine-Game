using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStartObject : MonoBehaviour
{
    public Sprite[] AnimationPanels;
    public GameObject AnimationPanel;

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
            AnimationPanel.GetComponent<CutsceneAnimation>().StartCutscene(AnimationPanels);
            AnimationPanel.SetActive(true);
        }
    }
}
