using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemLevel = -1;
    [SerializeField] private Image itemImage;

    private ItemData itemData;
    private float itemSoftCurrencyTime;

    private void Start()
    {
        SetLevel(itemLevel);
    }

    public void SetLevel(int level)
    {
        itemLevel = level;

        if (itemLevel < 0)
        {
            itemImage.sprite = null;
        }
        else
        {
            itemData = ItemManager.Instance.GetItemData(itemLevel);
            itemImage.sprite = itemData.itemThumbnail;
        }
    }

    public int GetLevel()
    {
        return itemLevel;
    }

    public float TimePassed(float deltaTime)
    {
        if (itemLevel >= 0)
        {
            itemSoftCurrencyTime += deltaTime;

            if (itemSoftCurrencyTime >= itemData.itemTime)
            {
                itemSoftCurrencyTime -= itemData.itemTime;
                return itemData.itemSoftCurrency;
            }
        }

        return 0;
    }

    public float SoftCurrencyPerSecond()
    {
        if (itemLevel >= 0)
        {
            return itemData.itemSoftCurrency / itemData.itemTime;
        }
        return 0;
    }
}
