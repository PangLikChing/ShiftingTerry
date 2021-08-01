using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    float canvasWidth, canvasHeight, buttonWidth, buttonHeight;
    RectTransform canvasRectTransform, startButtonRectTransform;

    void Awake()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        startButtonRectTransform = GetComponent<RectTransform>();

        //Fixing the UI
        canvasWidth = canvasRectTransform.rect.width * canvasRectTransform.localScale.x;
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;

        buttonWidth = canvasWidth / 2;
        buttonHeight = canvasHeight / 10;

        startButtonRectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);
        startButtonRectTransform.anchoredPosition = new Vector2(0, -canvasHeight / 4);
    }

    public void Pressed()
    {
        //Load game scene
        SceneManager.LoadScene(1);
    }
}
