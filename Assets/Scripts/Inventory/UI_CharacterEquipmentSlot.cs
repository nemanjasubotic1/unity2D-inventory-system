using System;
using UnityEngine.EventSystems;

public class UI_CharacterEquipmentSlot : ItemSlot
{
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;

    public class OnItemDroppedEventArgs
    {
        public Item_UI item_UI;
        public Item item;
        public Action<Item_UI> onWrongSlot;
        public Action<Item_UI> onRightSlot;
    }

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
            Item dropppedItem = item_UI.GetItem();

            OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs
            {
                item_UI = item_UI,
                item = dropppedItem,
                onWrongSlot = ReturnItemToParentSlot,
                onRightSlot = CorrectItemSlot
            });
        }

        // currentItemUI = eventData.pointerDrag.GetComponent<Item_UI>();
    }
}
