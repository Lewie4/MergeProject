using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrade", order = 2)]
public class UpgradeScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public string description;
    public List<Vector2> valueCost;
}
