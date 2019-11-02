using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        RefreshCost();
    }

    private void RefreshCost()
    {
        float cost = UpgradeManager.Instance.GetCost(upgrade);

        if (cost >= 0)
        {
            buttonText.text = cost.ToString();
        }
        else
        {
            button.interactable = false;
        }
    }

    public void OnClick()
    {
        if(UpgradeManager.Instance.TryUpgrade(upgrade))
        {
            RefreshCost();
        }
    }
}
