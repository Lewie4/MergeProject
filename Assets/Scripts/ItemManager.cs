﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemScriptableObject> itemScriptableObjects;
    public List<Item> items;

    public void HandleMove(Item currentItem, Item targetItem)
    {
        if (currentItem != targetItem)
        {
            if(CheckMerge(currentItem, targetItem))
            {
                //Merge
                targetItem.SetLevel(targetItem.GetLevel() + 1);
                targetItem.ResetPosition();

                currentItem.SetLevel(-1);
                currentItem.ResetPosition();
            }
            else
            {
                //Switch
                ItemContainer oldContainer = currentItem.GetItemContainer();
                currentItem.SetItemContainer(targetItem.GetItemContainer());
                targetItem.SetItemContainer(oldContainer);
            }
        }
    }

    private bool CheckMerge(Item currentItem, Item targetItem)
    {
        if (currentItem.GetLevel() == targetItem.GetLevel())
        {
            return true;
        }

        return false;
    }

    public ItemData GetItemData(int itemLevel)
    {
        return itemScriptableObjects[itemLevel].GetItemData();
    }

    public void SpawnItem(int level = 0)
    {
        foreach(Item item in items)
        {
            if (item.GetLevel() < 0)
            {
                item.SetLevel(level);
                break;
            }
        }
    }

    public float CheckSoftCurrency(float deltaTime)
    {
        float softCurrencyPerSecond = 0;

        foreach (Item item in items)
        {
            item.TimePassed(deltaTime);
            softCurrencyPerSecond += item.SoftCurrencyPerSecond();
        }

        return softCurrencyPerSecond;
    }
}