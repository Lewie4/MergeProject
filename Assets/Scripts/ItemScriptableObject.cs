using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/New Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] ItemData itemData;

    public ItemData GetItemData()
    {
        return itemData;
    }
}
