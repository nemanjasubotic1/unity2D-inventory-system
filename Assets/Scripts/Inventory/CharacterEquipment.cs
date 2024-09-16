using System;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    public event EventHandler<Item> OnEquipmentItemSlotEmpty;

    [SerializeField] private Transform[] medkitTransformArray;
    private SpriteRenderer[] medkitSpriteArray;

    [SerializeField] private Transform[] weaponTransformArray;
    private SpriteRenderer[] weaponSpriteArray;

    private Item currentWeaponItem;
    private Item currentMedkitItem;

    private void Awake()
    {
        medkitSpriteArray = new SpriteRenderer[medkitTransformArray.Length];
        weaponSpriteArray = new SpriteRenderer[weaponTransformArray.Length];

        for (int i = 0; i < medkitTransformArray.Length; i++)
        {
            medkitSpriteArray[i] = medkitTransformArray[i].GetComponent<SpriteRenderer>();
        }

        for (int i = 0; i < weaponTransformArray.Length; i++)
        {
            weaponSpriteArray[i] = weaponTransformArray[i].GetComponent<SpriteRenderer>();
        }
    }

    private void Start()
    {
        Item_UI.OnRemoveItemFromEquipmentSlot += Item_UI_OnRemoveItemFromEquipmentSlot;
    }

    private void OnDisable()
    {
        Item_UI.OnRemoveItemFromEquipmentSlot -= Item_UI_OnRemoveItemFromEquipmentSlot;
    }

    private void Item_UI_OnRemoveItemFromEquipmentSlot(object sender, Item e)
    {
        switch (e.itemType)
        {
            case Item.ItemType.Medkit:
                SetMedkitItem(null);
                break;
            case Item.ItemType.Sword:
                SetWeaponItem(null);
                break;
        }
    }

    public Item GetWeaponItem()
    {
        return currentWeaponItem;
    }

    public Item GetMedkitItem()
    {
        return currentMedkitItem;
    }

    public void SetWeaponItem(Item weaponItem)
    {
        if (weaponItem == null)
        {
            foreach (SpriteRenderer spriteRenderer in weaponSpriteArray)
            {
                spriteRenderer.sprite = null;
            }

            OnEquipmentItemSlotEmpty?.Invoke(this, currentWeaponItem);
        }
        else
        {
            for (int i = 0; i < medkitSpriteArray.Length; i++)
            {
                weaponSpriteArray[i].sprite = weaponItem.GetSprite();
            }
        }

        this.currentWeaponItem = weaponItem;
    }

    public void SetMedkitItem(Item medkitItem)
    {
        if (medkitItem == null)
        {
            foreach (SpriteRenderer spriteRenderer in medkitSpriteArray)
            {
                spriteRenderer.sprite = null;
            }

            OnEquipmentItemSlotEmpty?.Invoke(this, currentMedkitItem);
        }
        else
        {
            for (int i = 0; i < medkitSpriteArray.Length; i++)
            {
                medkitSpriteArray[i].sprite = medkitItem.GetSprite();
            }
        }

        this.currentMedkitItem = medkitItem;
    }

}
