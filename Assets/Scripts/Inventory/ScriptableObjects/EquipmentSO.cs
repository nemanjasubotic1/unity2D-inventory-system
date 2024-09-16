using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Scriptable Object")]
public class EquipmentSO : ScriptableObject
{
    public string equipmentName;
    public Item.ItemType itemType;

    public Sprite equipmentSprite;
}