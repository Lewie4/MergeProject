using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemScriptableObject> itemScriptableObjects;
    public List<ItemContainer> itemContainers;
}