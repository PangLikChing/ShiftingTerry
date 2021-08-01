using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    float canvasWidth, canvasHeight, buttonWidth, buttonHeight;
    RectTransform canvasRectTransform, exitButtonRectTransform;

    void Awake()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        exitButtonRectTransform = GetComponent<RectTransform>();

        //Fixing the UI
        canvasWidth = canvasRectTransform.rect.width * canvasRectTransform.localScale.x;
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;

        buttonWidth = canvasWidth / 2;
        buttonHeight = canvasHeight / 10;

        exitButtonRectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);
        exitButtonRectTransform.anchoredPosition = new Vector2(0, -canvasHeight / 10 * 4);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
