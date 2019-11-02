using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemVisual : EventTrigger
{
    [SerializeField] ItemContainer currentContainer;
    [SerializeField] ItemContainer targetContainer;

    private void Start()
    {
        currentContainer = transform.parent.gameObject.GetComponent<ItemContainer>();
    }

    private void Move()
    {
        if(targetContainer == null || targetContainer == currentContainer)
        {
            SetContainer(currentContainer);
            return;
        }

        ItemManager.Instance.HandleMove(currentContainer, targetContainer);

        targetContainer = null;
    }

    public void SetContainer(ItemContainer itemContainer)
    {
        currentContainer = itemContainer;
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = currentContainer.transform.position;
        transform.SetParent(currentContainer.transform);
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
