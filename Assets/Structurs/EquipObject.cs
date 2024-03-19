using UnityEngine;
[System.Serializable]
public struct EquipObject
{
    [SerializeField] public int ID_Slot;
    [SerializeField] public Equip_Status slotStatus;
    [SerializeField] public Item_Object Item;
    [SerializeField] public LimitedNumber qantity_Item;
}