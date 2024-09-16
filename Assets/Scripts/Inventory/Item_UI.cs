using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_UI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static event EventHandler<Item> OnRemoveItemFromEquipmentSlot;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Image image;
    private TextMeshProUGUI amountText;

    private Item currentItem;
    private ItemSlot currentItemSlot;

    private Transform parent;

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        image = transform.Find("image").GetComponent<Image>();
        amountText = transform.Find("amountText").GetComponent<TextMeshProUGUI>();

        parent = transform.parent;
    }

    public void SetItem(Item item, ItemSlot itemSlot)
    {
        this.currentItem = item;
        image.sprite = item.GetSprite();
        if (item.amount == 1)
        {
            amountText.SetText("");
        }
        else
        {
            amountText.SetText(item.amount.ToString());
        }

        itemSlot.SetCurrentItemUI(this);
        SetItemSlot(itemSlot);
    }

    public void SetItemSlot(ItemSlot itemSlot)
    {
        this.currentItemSlot = itemSlot;
    }

    public void RemoveItemUIFromItemSlot()
    {
        if (currentItemSlot.IsEquipment())
        {
            OnRemoveItemFromEquipmentSlot?.Invoke(this, currentItem);
        }
       
        currentItemSlot.RemoveItemUI();
    }

    public Item GetItem()
    {
        return currentItem;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // registruje samo UI objekte, ne World game objekte
        GameObject endDragUIElement = eventData.pointerEnter;

        if (endDragUIElement == null)
        {
            SetParent(parent);
        }
        else
        {
            Transform endDragTransformParent = endDragUIElement.transform.parent;
            if (!endDragTransformParent.TryGetComponent<ItemSlot>(out ItemSlot itemSlot))
            {
                SetParent(parent);
            }
        }
    }

    public void SetParent(Transform newParent)
    {
        gameObject.transform.SetParent(newParent);
        rectTransform.anchoredPosition = Vector2.zero;
        this.parent = newParent;
    }

    public Transform GetPrevoiusParent()
    {
        return parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log(item.itemType);
    }
}
