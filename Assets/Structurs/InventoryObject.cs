using UnityEngine;
[System.Serializable]
public struct InventoryObject
{
    [SerializeField] public int ID_Slot;
    [SerializeField] public Slot_Status slotStatus;
    [SerializeField] public Item_Object Item;
    [SerializeField] public LimitedNumber qantity_Item;
}