using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    float canvasWidth, optionBUttonHeigthLength;
    RectTransform canvasRectTransform, optionsButtonRectTransform;

    void Awake()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        optionsButtonRectTransform = GetComponent<RectTransform>();

        //Fixing the UI
        canvasWidth = canvasRectTransform.rect.width * canvasRectTransform.localScale.x;

        optionBUttonHeigthLength = canvasWidth / 15 * 2;

        optionsButtonRectTransform.sizeDelta = new Vector2(optionBUttonHeigthLength, optionBUttonHeigthLength);
        optionsButtonRectTransform.anchoredPosition = new Vector2(-canvasWidth / 20, -canvasWidth / 20);
    }
}
