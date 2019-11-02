using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Item item;

    private RectTransform rectTransform;

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
}
