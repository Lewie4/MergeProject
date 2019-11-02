using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private CurrencyText text;


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

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        item.GetComponent<ItemVisual>().SetContainer(this);
    }

    public void ResetItemPosition()
    {
        item.GetComponent<ItemVisual>().ResetPosition();
    }

    public void UpgradeItem()
    {
        SetItemLevel(item.GetLevel() + 1);
    }

    public void RemoveItem()
    {
        SetItemLevel(-1);
    }

    private void SetItemLevel(int level)
    {
        item.SetLevel(level);
    }

    public void TimePassed(float deltaTime)
    {
        double softCurrencyToAdd = item.TimePassed(deltaTime) * itemSoftCurrencyBonus;

        if (softCurrencyToAdd > 0)
        {
            GameManager.Instance.AddSoftCurrency(softCurrencyToAdd);
            text.GainCurrency(softCurrencyToAdd.ToString());
        }
    }
}
