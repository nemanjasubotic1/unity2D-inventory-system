using System.Collections.Generic;
using System.Linq;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{

    [SerializeField] private Transform item_UIprefab;

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private PlayerController playerController;
    private UI_CharacterEquipment uI_CharacterEquipment;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        uI_CharacterEquipment = FindFirstObjectByType<UI_CharacterEquipment>();

    }

    private void OnDisable()
    {
        inventory.OnItemListChanged -= Inventory_OnItemListChanged;
        Item_UI.OnRemoveItemFromEquipmentSlot -= Item_UI_OnRemoveItemFromEquipmentSlot;
        uI_CharacterEquipment.OnAddItemInEquipmentSlot -= UI_CharacterEquipment_OnAddItemInEquipmentSlot;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (Item item in inventory.GetItemList())
            {
                Debug.Log(item.itemType + " amount: " + item.amount);
            }

            if (!inventory.GetItemList().Any())
            {
                Debug.Log("No items");
            }
        }
    }

    public void SetPlayer(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        Item_UI.OnRemoveItemFromEquipmentSlot += Item_UI_OnRemoveItemFromEquipmentSlot;
        uI_CharacterEquipment.OnAddItemInEquipmentSlot += UI_CharacterEquipment_OnAddItemInEquipmentSlot;

        PopulateInventoryWithItems();

    }

    private void UI_CharacterEquipment_OnAddItemInEquipmentSlot(object sender, Item e)
    {
        inventory.RemoveItem(e);
    }

    private void Item_UI_OnRemoveItemFromEquipmentSlot(object sender, Item e)
    {
       inventory.AddItem(e, false);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void Inventory_OnItemListChanged(object sender, Item item)
    {
        PopulateInventoryWithItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform transform in itemSlotContainer)
        {
            if (transform == itemSlotTemplate) continue;

            Destroy(transform.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 65f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                // Use item
                inventory.UseItem(item);
            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                // Drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };

                inventory.RemoveItem(item);
                ItemWorld.DropItem(playerController.transform.position, duplicateItem);
            };


            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * -1 * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI amountText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount == 1)
            {
                amountText.SetText("");
            }
            else
            {
                amountText.SetText(item.amount.ToString());
            }

            x++;

            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

    private void PopulateInventoryWithItems()
    {
        List<Transform> itemSlotTemplateList = new();
        List<ItemSlot> itemSlotList = new();

        foreach (Transform itemSlotTemplate in itemSlotContainer)
        {
            itemSlotTemplateList.Add(itemSlotTemplate);
            itemSlotList.Add(itemSlotTemplate.GetComponent<ItemSlot>());
        }

        for (int i = 0; i < inventory.GetItemList().Count; i++)
        {
            Item item = inventory.GetItemList()[i];

            if (!AddingItemsToAlreadyInventory(item, itemSlotList))
            {
                for (int j = 0; j < itemSlotTemplateList.Count; j++)
                {
                    Transform currentItemUITransform = itemSlotTemplateList[j].transform.Find("pfItem_UI(Clone)");

                    if (currentItemUITransform == null)
                    {
                        Transform item_UITransform = Instantiate(item_UIprefab, itemSlotTemplateList[j]);
                        Item_UI item_UI = item_UITransform.GetComponent<Item_UI>();
                        item_UI.SetItem(item, itemSlotTemplateList[j].GetComponent<ItemSlot>());

                        item_UI.GetComponent<ButtonFunctions>().OnLeftMouseClick = () =>
                        {
                            // Use item
                        };


                        item_UI.GetComponent<ButtonFunctions>().OnRightMouseClick = () =>
                        {
                            // Drop item
                            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount, weaponSO = item.weaponSO };

                            inventory.RemoveItem(item);
                            ItemWorld.DropItem(playerController.transform.position, duplicateItem);

                            item_UI.RemoveItemUIFromItemSlot();
                            Destroy(item_UI.gameObject);
                        };

                        break;
                    }
                }
            }
        }
    }

    private bool AddingItemsToAlreadyInventory(Item item, List<ItemSlot> itemSlotList)
    {
        for (int i = 0; i < itemSlotList.Count; i++)
        {
            Item_UI item_UI = itemSlotList[i].GetCurrentItemUI();

            if (item_UI != null && item_UI.GetItem() == item)
            {
                item_UI.SetItem(item, itemSlotList[i]);
                return true;
            }
        }
        return false;
    }
}
