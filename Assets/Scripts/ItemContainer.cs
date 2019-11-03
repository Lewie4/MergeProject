using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemContainer : MonoBehaviour
{
    private RectTransform rectTransform;
    private float itemSoftCurrencyBonus = 1;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }

}
