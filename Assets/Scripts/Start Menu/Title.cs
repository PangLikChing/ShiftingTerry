using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    float canvasWidth, canvasHeight, titleWidth, titleHeight;
    RectTransform canvasRectTransform, TitleRectTransform;

    void Awake()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        TitleRectTransform = GetComponent<RectTransform>();

        //Fixing the UI
        canvasWidth = canvasRectTransform.rect.width * canvasRectTransform.localScale.x;
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;

        titleWidth = canvasWidth / 10 * 9;
        titleHeight = canvasHeight / 10;

        TitleRectTransform.sizeDelta = new Vector2(titleWidth, titleHeight);
        TitleRectTransform.anchoredPosition = new Vector2(0, canvasHeight / 5);
    }
}
