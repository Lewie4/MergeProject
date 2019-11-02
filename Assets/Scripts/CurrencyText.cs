using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Animator anim;

    public void GainCurrency(string value)
    {
        text.text = value;
        anim.SetTrigger("Gain");
    }
}
