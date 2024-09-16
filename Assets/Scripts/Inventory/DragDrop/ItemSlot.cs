using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] protected Item_UI currentItemUI;
    [SerializeField] private Item currentItem;

    [SerializeField] private bool isCharacterEquipment = false;

    public void SetCurrentItemUI(Item_UI item_UI)
    {
        currentItemUI = item_UI;
        SetItem();
    }

    public void RemoveItemUI()
    {
        currentItemUI = null;
        currentItem = null;
    }

    private void SetItem()
    {
        currentItem = currentItemUI.GetItem();
    }

    public Item GetItem()
    {
        return currentItem;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
    }

    protected void ReturnItemToParentSlot(Item_UI droppedItemUI)
    {
        if (droppedItemUI != null)
        {
            droppedItemUI.SetParent(droppedItemUI.GetPrevoiusParent());
        }
    }

    protected void CorrectItemSlot(Item_UI item_UI)
    {
        item_UI.RemoveItemUIFromItemSlot();
        SetCurrentItemUI(item_UI);
        item_UI.SetItemSlot(this);
        item_UI.SetParent(transform);
    }

    public Item_UI GetCurrentItemUI()
    {
        return currentItemUI;
    }

    public bool IsEquipment()
    {
        return isCharacterEquipment;
    }

}
