using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemVisual : EventTrigger
{
    [SerializeField] ItemContainer currentContainer;
    [SerializeField] ItemContainer targetContainer;

    public void Spawn(ItemContainer container)
    {
        currentContainer = container;
    }

    private void Move()
    {
        if (targetContainer != null || targetContainer == currentContainer)
        {
            if (currentContainer == null)
            {
                currentContainer = targetContainer;
            }

            if (CheckMerge(targetContainer))
            {
                targetContainer.Merge(this.gameObject);
                currentContainer.Remove();

                currentContainer = targetContainer;                
            }
        }

        targetContainer = null;

        transform.position = currentContainer.transform.position;
    }

    public bool CheckMerge(ItemContainer targetContainer)
    {
        if (targetContainer != null)
        {
            return true;
        }

        return false;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        transform.position += (Vector3)eventData.delta;

        foreach(ItemContainer itemContainer in ItemManager.Instance.itemContainers)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(itemContainer.GetRectTransform(), Input.mousePosition))
            {
                targetContainer = itemContainer;
                break;
            }

            targetContainer = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        Move();
    }
}
