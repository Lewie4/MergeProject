using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdProvider : MonoBehaviour
{
    protected virtual void Start()
    {

    }

    public virtual bool IsReady(string placement)
    {
        return false;
    }

    public virtual void ShowAd(string placement)
    {

    }
}
