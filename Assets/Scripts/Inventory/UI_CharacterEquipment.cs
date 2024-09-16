using System;
using Cinemachine;
using UnityEngine;

public class UI_CharacterEquipment : MonoBehaviour
{
    public event EventHandler<Item> OnAddItemInEquipmentSlot;

    [SerializeField] private CinemachineVirtualCamera characterVirtualCamera;

    private UI_CharacterEquipmentSlot weaponSlot;
    private UI_CharacterEquipmentSlot medkitSlot;

    private CharacterEquipment characterEquipment;

    private bool isActive = true;

    private void Awake()
    {
        OpenEquipmentSheet();

        weaponSlot = transform.Find("WeaponSlot").GetComponent<UI_CharacterEquipmentSlot>();
        medkitSlot = transform.Find("MedkitSlot").GetComponent<UI_CharacterEquipmentSlot>();

        weaponSlot.OnItemDropped += WeaponSlot_OnItemDroppped;
        medkitSlot.OnItemDropped += MedkitSlot_OnItemDropped;
    }

    private void OnDestroy()
    {
        characterEquipment.OnEquipmentItemSlotEmpty -= CharacterEquipment_OnEquipmentItemSlotEmpty;
        weaponSlot.OnItemDropped -= WeaponSlot_OnItemDroppped;
        medkitSlot.OnItemDropped -= MedkitSlot_OnItemDropped;
    }

    public void SetCharacterEquipment(CharacterEquipment characterEquipment)
    {
        this.characterEquipment = characterEquipment;
        characterEquipment.OnEquipmentItemSlotEmpty += CharacterEquipment_OnEquipmentItemSlotEmpty;
    }

    private void CharacterEquipment_OnEquipmentItemSlotEmpty(object sender, Item e)
    {
        switch (e.itemType)
        {
            case Item.ItemType.Medkit:
                medkitSlot.transform.Find("emptyImage").gameObject.SetActive(true);
                break;
            case Item.ItemType.Sword:
                weaponSlot.transform.Find("emptyImage").gameObject.SetActive(true);
                break;
        }
    }

    public void OpenEquipmentSheet()
    {
        isActive = !isActive;

        if (isActive)
        {
            characterVirtualCamera.Priority = 12;
        }
        else
        {
            characterVirtualCamera.Priority = 9;
        }

        gameObject.SetActive(isActive);
    }

    private void MedkitSlot_OnItemDropped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        if (e.item.itemType != Item.ItemType.Medkit)
        {
            e.onWrongSlot(e.item_UI);
        }
        else
        {
            e.onRightSlot(e.item_UI);
            e.item_UI.transform.localScale = Vector2.one * 2f;

            medkitSlot.transform.Find("emptyImage").gameObject.SetActive(false);

            characterEquipment.SetMedkitItem(e.item);

            // remove item from inventory as well
            OnAddItemInEquipmentSlot?.Invoke(this, e.item);
        }
    }

    private void WeaponSlot_OnItemDroppped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {

        if (e.item.itemType != Item.ItemType.Sword)
        {
            e.onWrongSlot(e.item_UI);
        }
        else
        {
            e.onRightSlot(e.item_UI);
            e.item_UI.transform.localScale = Vector2.one * 2f;

            weaponSlot.transform.Find("emptyImage").gameObject.SetActive(false);

            characterEquipment.SetWeaponItem(e.item);

            // remove item from inventory as well
            OnAddItemInEquipmentSlot?.Invoke(this, e.item);
        }
    }
}

