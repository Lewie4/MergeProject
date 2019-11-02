using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemScriptableObject> itemScriptableObjects;
    public List<ItemContainer> itemContainers;

    public void HandleMove(ItemContainer currentContainer, ItemContainer targetContainer)
    {
        if (currentContainer != targetContainer)
        {
            if(CheckMerge(currentContainer, targetContainer))
            {
                //Merge
                targetContainer.UpgradeItem();
                targetContainer.ResetItemPosition();

                currentContainer.RemoveItem();
                currentContainer.ResetItemPosition();
            }
            else
            {
                //Switch
                Item oldItem = currentContainer.GetItem();
                currentContainer.SetItem(targetContainer.GetItem());
                targetContainer.SetItem(oldItem);
            }
        }
    }

    private bool CheckMerge(ItemContainer currentContainer, ItemContainer targetContainer)
    {
        if (targetContainer.GetItem().GetLevel() == currentContainer.GetItem().GetLevel())
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
        foreach(ItemContainer itemContainer in itemContainers)
        {
            Item item = itemContainer.GetItem();
            if (item.GetLevel() < 0)
            {
                item.SetLevel(level);
                break;
            }
        }
    }

    public void CheckSoftCurrency(float deltaTime)
    {
        foreach (ItemContainer itemContainer in itemContainers)
        {
            itemContainer.TimePassed(deltaTime);
        }
    }
}