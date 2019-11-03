using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemVisual : EventTrigger
{
    [SerializeField] Item targetItem;

    private Item item;

    private void Awake()
    {
        item = GetComponent<Item>();
    }

    private void Move()
    {
        if(targetItem == null || targetItem == item)
        {
            ResetPosition();            
            return;
        }

        ItemManager.Instance.HandleMove(item, targetItem);

        targetItem = null;
    }

    public void ResetPosition()
    {
        transform.position = item.GetItemContainer().transform.position;
        transform.SetParent(item.GetItemContainer().transform);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        transform.SetParent(ItemManager.Instance.transform);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        transform.position += (Vector3)eventData.delta;

        foreach(Item item in ItemManager.Instance.items)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(item.GetContainerTransform(), Input.mousePosition))
            {
                targetItem = item;
                break;
            }

            targetItem = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        Move();
    }
}
