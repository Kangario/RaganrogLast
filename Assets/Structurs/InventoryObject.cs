using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct InventoryObject
{
    [SerializeField] public int ID_Slot;
    [SerializeField] public Slot_Status slotStatus;
    [SerializeField] public Item_Object Item;
    [SerializeField] public Label Slot;
    [SerializeField] public LimitedNumber qantity_Item;

    public InventoryObject(int ID_Slot, Slot_Status slotStatus, Item_Object Item, Label Slot, LimitedNumber qantity_Item)
    {
        this.ID_Slot = ID_Slot;
        this.slotStatus = slotStatus;
        this.Item = Item;
        this.Slot = Slot;
        this.qantity_Item = qantity_Item;
    }

}