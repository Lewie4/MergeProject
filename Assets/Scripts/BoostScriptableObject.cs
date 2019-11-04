using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "ScriptableObjects/Boost")]
public class BoostScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public string description;
    public Boost upgradeType;
    public float power;
    public float duration;
    public float adPower;
    public float adDuration;
}
