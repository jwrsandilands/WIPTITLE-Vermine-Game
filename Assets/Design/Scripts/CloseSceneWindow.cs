using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CloseSceneWindow : MonoBehaviour
{
	public GameObject sceneWindow;
	public Button backButton;

	void Start()
	{
		Button btn = backButton.GetComponent<Button>();
		btn.onClick.AddListener(CloseScene);
	}

	void CloseScene()
	{
		sceneWindow.GetComponent<SceneWindowObject>().showPopupFlag = false;
	}
}
