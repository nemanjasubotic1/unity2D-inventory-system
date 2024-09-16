using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : MonoBehaviour
{
    private Transform uiCharacterEquipment;
    private UI_CharacterEquipment uI_CharacterEquipment;

    private void Awake()
    {
        uiCharacterEquipment = transform.Find("UI_CharacterEquipment");
        uI_CharacterEquipment = uiCharacterEquipment.GetComponent<UI_CharacterEquipment>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            uI_CharacterEquipment.OpenEquipmentSheet();
        }
    }
}
