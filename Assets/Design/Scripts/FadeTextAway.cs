using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeTextAway : MonoBehaviour
{
    public decimal fadeTime;
    public bool notificationTrigger = false;
    private bool waitComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, 0);
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        if (notificationTrigger)
        {
            notificationTrigger = false;
            StartCoroutine(TextFadeCoroutine());
        }
        if (waitComplete && gameObject.GetComponent<TextMeshProUGUI>().color.a >= 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, gameObject.GetComponent<TextMeshProUGUI>().color.a - 0.01f);
        }
        else
        {
            waitComplete = false;
        }
    }

    IEnumerator TextFadeCoroutine()
    {
        yield return new WaitForSeconds(3);
        waitComplete = true;
    }
}
