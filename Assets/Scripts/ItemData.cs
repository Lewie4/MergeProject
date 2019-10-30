using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    //Variables
    [SerializeField] private int itemID;
    [SerializeField] private Sprite itemThumbnail;
    [SerializeField] private string itemName;
    [SerializeField] private double itemValue;
    [SerializeField] private float itemTime;
}