using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemLevel = -1;
    [SerializeField] private Image itemImage;
    [SerializeField] private CurrencyText text;
    [SerializeField] private ItemContainer itemContainer;

    private ItemData itemData;
    private float itemSoftCurrencyTime;
    private ItemVisual itemVisual;

    private void Awake()
    {
        itemVisual = GetComponent<ItemVisual>();
    }

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
            itemImage.enabled = false;
        }
        else
        {            
            itemData = ItemManager.Instance.GetItemData(itemLevel);
            itemImage.sprite = itemData.itemThumbnail;
            itemImage.enabled = true;
        }
    }

    public int GetLevel()
    {
        return itemLevel;
    }

    public void TimePassed(float deltaTime)
    {  
        if (itemLevel >= 0)
        {
            itemSoftCurrencyTime += deltaTime;

            if (itemSoftCurrencyTime >= ItemManager.CurrencyTime)
            {
                itemSoftCurrencyTime -= ItemManager.CurrencyTime;

                float softCurrencyToAdd = CalculateSoftCurrency();
                GameManager.Instance.AddSoftCurrency(softCurrencyToAdd);
                text.GainCurrency(softCurrencyToAdd.ToString());
            }
        }
        else if (itemSoftCurrencyTime > 0)
        {
            itemSoftCurrencyTime = 0;
        }
    }

    private float CalculateSoftCurrency()
    {
        return ItemManager.GetCurrencyForLevel(itemLevel) * TempUpgradeManager.Instance.GetTempBoostMultiplier(TempUpgrade.GoldMult); //Container bonus? * itemSoftCurrencyBonus;
    }

    public float SoftCurrencyPerSecond()
    {
        if (itemLevel >= 0)
        {
            return CalculateSoftCurrency() / ItemManager.CurrencyTime;
        }
        return 0;
    }

    public void ResetPosition()
    {
        itemVisual.ResetPosition();
    }

    public ItemContainer GetItemContainer()
    {
        return itemContainer;
    }

    public void SetItemContainer(ItemContainer newContainer)
    {
        itemContainer = newContainer;
        ResetPosition();
    }

    public RectTransform GetContainerTransform()
    {
        return itemContainer.GetRectTransform();
    }
}
