using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TempUpgrade", menuName = "ScriptableObjects/Temp Upgrade")]
public class TempUpgradeScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public string description;
    public TempUpgrade upgradeType;
    public float power;
    public float duration;
}
