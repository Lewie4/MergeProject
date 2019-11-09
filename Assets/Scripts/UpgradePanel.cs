using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descText;

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        float cost = UpgradeManager.Instance.GetCost(upgrade, 1);

        if (cost >= 0)
        {
            costText.text = cost.ToString();
        }
        else
        {
            costText.text = "Max Level";
            button.interactable = false;
        }

        image.sprite = UpgradeManager.Instance.GetSprite(upgrade);

        levelText.text = "Level " + (UpgradeManager.Instance.GetUpgradeLevel(upgrade) + 1);

        descText.text = UpgradeManager.Instance.GetDescription(upgrade);
    }

    public void OnClick()
    {
        if(UpgradeManager.Instance.TryUpgrade(upgrade))
        {
            Refresh();
        }
    }
}
