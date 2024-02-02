using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPickupObject : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject textElement;
    public GameObject cameraFocusObject;
    public GameObject UIManager;
    public Button collectable;
    public Image popupScene;
    public bool isSceneWindowActive;

    void Start()
    {
        Button btn = collectable.GetComponent<Button>();
        btn.onClick.AddListener(Collect);
    }

    private void Collect()
    {
        print(gameObject.name);
        textElement.GetComponent<TextMeshProUGUI>().text = $"{gameObject.name} Collected!";
        textElement.GetComponent<FadeTextAway>().notificationTrigger = true;
        textElement.GetComponent<TextMeshProUGUI>().color = Color.white;

        GameObject created = Instantiate(cameraFocusObject);
        created.transform.SetParent(collectable.transform, false);
        created.GetComponent<UIAnimatePickup>().playerCamera = playerCamera;
        created.GetComponent<UIAnimatePickup>().popupScene = popupScene;

        gameObject.GetComponent<Image>().enabled = false;
        collectable.enabled = false;
    }
}
