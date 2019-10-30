using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : EventTrigger
{
    [SerializeField] ItemContainer currentContainer;
    [SerializeField] ItemContainer targetContainer;

    private void Move()
    {
        if(targetContainer)
        {
            currentContainer = targetContainer;
            targetContainer = null;
        }

        transform.position = currentContainer.transform.position;
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
