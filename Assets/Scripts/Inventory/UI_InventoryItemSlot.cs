using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventoryItemSlot : ItemSlot
{

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        Item_UI item_UI = eventData.pointerDrag.GetComponent<Item_UI>();

        if (currentItemUI != null)
        {
            item_UI.SetParent(item_UI.GetPrevoiusParent());
        }
        else
        {
            CorrectItemSlot(item_UI);
            item_UI.transform.localScale = Vector2.one;
        }
    }
}
