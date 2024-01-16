using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Inventory_Controller : MonoBehaviour
{
    [SerializeField] private InventObject[] items;
    [SerializeField] private GameObject[] slots_Invent;
    [SerializeField] private GameObject button_Take;
    private ItemPicker items_Picker;
    private InventoryAddFunctions inventoryAddFunctions;
    private void Start()
    {
        slots_Invent = Slot_Counter.slots.ToArray();
        items_Picker = new ItemPicker(items,button_Take);
        inventoryAddFunctions = new InventoryAddFunctions(slots_Invent, items);
        Event_Items.item_Take += items_Picker.CanTake;
        button_Take.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(onClickButtonTake);
        
    }
    public void onClickButtonTake()
    {
        items_Picker.TakeItem();
        inventoryAddFunctions.AddItemInInventory();
    }
    public void SwapItems(int currentIndex, int secondIndex)
    {
        InventObject temp =  items[currentIndex];
        items[currentIndex] = items[secondIndex];
        items[secondIndex] = temp;
    }
}
class InventoryAddFunctions
{
    private GameObject[] slots_Invent;
    private InventObject[] items;
    
    public InventoryAddFunctions(GameObject[] slots_Invent, InventObject[] items)
    {
        this.slots_Invent = slots_Invent;
        this.items = items;
    }
    public void AddItemInInventory()
    {
        Dictionary<GameObject, bool> occupiedSlot = new Dictionary<GameObject, bool>();
        for (int i =0; i<slots_Invent.Length; i++)
        {
            Inventory_Object_Component show_Item = slots_Invent[i].GetComponent<Inventory_Object_Component>();
           
            if (show_Item.invent_Object.item_Setting == null)
            {
                occupiedSlot.Add(slots_Invent[i],false);
            }
            else
            {
                occupiedSlot.Add(slots_Invent[i], true);
            }
        }

        CheckedSlot(occupiedSlot);
    }
    private void CheckedSlot(Dictionary<GameObject, bool> occupiedSlot)
    {
        int length = 0;
        foreach (KeyValuePair<GameObject, bool> slot in occupiedSlot)
        {
            Inventory_Object_Component slot_Inventory = slots_Invent[length].GetComponent<Inventory_Object_Component>();
            if (slot.Value == false)
            {
                slot_Inventory.invent_Object = items[length];
                slot_Inventory.invent_Object.inInventory.value++;
                slot_Inventory.ItemDisplay();
            }
            else
            {

                if (slot_Inventory.invent_Object.quantity_Item.ValueMoreTreshold() == false)
                {                    
                    if (slot_Inventory.invent_Object.inInventory.value < items[length].quantity_Item.value)
                    {
                        slot_Inventory.invent_Object.quantity_Item.value++;
                        slot_Inventory.invent_Object.inInventory.value++;
                        slot_Inventory.ItemDisplay();
                    }
                }
                else
                {
                    slot_Inventory.invent_Object.quantity_Item.LimitNumber();
                }
            }
            length++;
        }
    }



}
class ItemPicker :  IItemPicker
{
    private InventObject[] items;
    private GameObject targetItem;
    private GameObject button_Take;
    private bool canTake = false;

    public ItemPicker(InventObject[] items, GameObject button_Take)
    {
        this.items = items;
        this.button_Take = button_Take;
       
    }
    public void CanTake(GameObject targetItem)
    {
        if (!canTake)
        {
            canTake = true;
            button_Take.SetActive(true);
            this.targetItem = targetItem;
        }
        else
        {
            canTake = false;
            button_Take.SetActive(false);
            this.targetItem = null;
        }
    }
    public void TakeItem()
    {

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item_Setting == null)
            {
                AddItemInArray(i);
                break;
            }
            else
            {
                if (items[i].item_Setting == targetItem.GetComponent<RItem_Component>().item && items[i].quantity_Item.ValueMoreTreshold() == false)
                {
                    items[i].quantity_Item.value++;
                    items[i].inInventory.Threshold = items[i].quantity_Item.Threshold;
                    Object.Destroy(targetItem);
                    button_Take.SetActive(false);
                    break;
                }
                else
                {
                    items[i].quantity_Item.LimitNumber();

                }
            }
        }

    }
    public void AddItemInArray(int index_Slot)
    {
        items[index_Slot] = new InventObject();
        items[index_Slot].item_Setting = targetItem.GetComponent<RItem_Component>().item;
        items[index_Slot].box_Collider_Size = targetItem.GetComponent<BoxCollider2D>().size;
        items[index_Slot].quantity_Item = items[index_Slot].item_Setting.qantity_Item;
        items[index_Slot].inInventory.Threshold = items[index_Slot].quantity_Item.Threshold;
        Object.Destroy(targetItem);
        button_Take.SetActive(false);
    }
}

