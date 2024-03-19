using RPlayer;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Inventory_Controller : MonoBehaviour
{
    [SerializeField] GameObject takeButton;
    public InventoryObject[] items;
    public List<SlotComponent> slots;
    private List<GameObject> takedItem = new List<GameObject>();

    public void Start()
    {
        Event_Items.InItem += InItem;
        Event_Items.OutItem += OutItem;
        slots = SlotCounter.slots;
        CreateInventory();
    }

    public void TakeItem()
    {
       for (int i =0; i < items.Length; i++)
        {
           //Если слот пустой то просто добавляем
                if (CheckSlots(items[i]))
                {

                    AddItem(takedItem[takedItem.Count - 1], i, items[i].slotStatus);
                    break;

                }
                else
                {   
                //Если слот не пустой проверяем количество предметов в слоте и можем ли мы туда их положить
                    if (CheckValue(items[i]))
                    {
                        AddItem(takedItem[takedItem.Count - 1], i, items[i].slotStatus);
                        break;
                    }
                }
            
        }
    }

    public bool CheckValue(InventoryObject item)
    {   
        //Проверка если предмет который мы подобрали такой же как и в инвенторе то тогда увеличиваем их колличество
        if (takedItem[takedItem.Count - 1].GetComponent<RItem_Component>().item == item.Item)
        {
            if (item.qantity_Item.value < item.qantity_Item.Threshold)
            {
                return true;
            }
            else
            {
                item.slotStatus = Slot_Status.Filled;
                item.qantity_Item.LimitNumber();
                return false;
            }
        }
        else
        {
            //Если такого предмета в инветаре нет то пропускаем
            return false;
        }
    }

    public bool CheckSlots(InventoryObject item)
    {
            if (item.slotStatus == Slot_Status.NoItem)
            return true;
            else
            return false;
    }

    public void AddItem(GameObject item , int i, Slot_Status ItemStatus)
    {
        if (slots[i].typeSlot == SlotType.InventSlot)
        {
            switch (ItemStatus)
            {
                case Slot_Status.Unfilled:
                    Debug.Log("Добавляем +1 к " + i);
                    items[i].qantity_Item.value += item.GetComponent<RItem_Component>().item.qantity_Item.value;
                    UpdateInventory(i);
                    Destroy(item);
                    break;
                case Slot_Status.NoItem:
                    Debug.Log("Добавляем итем");
                    items[i].Item = item.GetComponent<RItem_Component>().item;
                    items[i].qantity_Item = item.GetComponent<RItem_Component>().item.qantity_Item;
                    items[i].slotStatus = Slot_Status.Unfilled;
                    UpdateInventory(i);
                    Destroy(item);
                    break;
            }
        }
    }

    public void AddItemSlot(SlotComponent item1, SlotComponent item2, SlotType SlotStatus)
    {   // Проверяем статуст слота, инвентарь - ЭКИПИРОВКА, или же экипировка - ИНВЕНТАРЬ
        switch (SlotStatus)
        {
            case SlotType.EquipSlot:
                // Если у нас слот экипировки равен пустоте то добовляем
                
                if (item2.typeItem == item1.item.Item.type)
                {
                    if (item2.item.Item == null)
                    {
                        items[item2.ID_Slot] = items[item1.ID_Slot];
                        items[item2.ID_Slot].qantity_Item.value -= (items[item2.ID_Slot].qantity_Item.value - 1);
                        items[item1.ID_Slot].qantity_Item.value -= 1;
                        //Так же удаляем объект в инвентаре если у него 0 итемов
                        if (items[item1.ID_Slot].qantity_Item.value == 0)
                        {
                            items[item1.ID_Slot] = new InventoryObject();
                        }
                    }
                }

                break;
            case SlotType.InventSlot:
                //если у нас и слот экипировки и слот инвентаря равны
                if (item2.item.Item == item1.item.Item)
                {   
                    //Мы проверяем не заполнен ли слот инвенторя и если нет добавляемм в ином случае ниче не происходит
                    if (items[item2.ID_Slot].qantity_Item.value < items[item2.ID_Slot].qantity_Item.Threshold)
                    {
                        items[item2.ID_Slot].qantity_Item.value += items[item1.ID_Slot].qantity_Item.value;
                        items[item1.ID_Slot] = new InventoryObject();
                    }
                }
                else
                {
                    //Если не равны слоты, то перебираемм все и находим либо пустой слот с типом Инветарь или же находим похожие слоты и проверяем их на заполненность
                    for (int i = 0; i < slots.Count; i++)
                    {
                        if (slots[i].GetComponent<SlotComponent>().typeSlot == SlotType.InventSlot)
                        {
                            
                                if (items[i].Item == null)
                                {
                                    items[i] = items[item1.ID_Slot];
                                    items[item1.ID_Slot] = new InventoryObject();
                                }
                                else if (items[i].Item == items[item1.ID_Slot].Item)
                                {
                                if (items[i].qantity_Item.value < items[i].qantity_Item.Threshold)
                                {
                                    items[i].qantity_Item.value += items[item1.ID_Slot].qantity_Item.value;
                                    items[item1.ID_Slot] = new InventoryObject();
                                }
                                }
                            }
                        
                    }
                }
                break;
        }
            
        
    }

    public void SwapItemSlot(SlotComponent item1, SlotComponent item2)
    {
        if (item1.item.Item == item2.item.Item)
        {
            //Изменить все на Items а не на slots в inventory_Object_Component
            if (item2.item.qantity_Item.value + item1.item.qantity_Item.value <= item2.item.qantity_Item.Threshold)
            {
                items[item2.ID_Slot].qantity_Item.value += items[item1.ID_Slot].qantity_Item.value;
                items[item1.ID_Slot] = new InventoryObject();
            }
            else if (item2.item.qantity_Item.value != item2.item.qantity_Item.Threshold && item1.item.qantity_Item.value != item2.item.qantity_Item.Threshold)
            {
                float tempValue = items[item2.ID_Slot].qantity_Item.Threshold - items[item2.ID_Slot].qantity_Item.value;
                float tempValue2 = items[item1.ID_Slot].qantity_Item.value - tempValue;
                items[item2.ID_Slot].qantity_Item.value += (items[item1.ID_Slot].qantity_Item.value - tempValue2);
                items[item1.ID_Slot].qantity_Item.value -= (items[item1.ID_Slot].qantity_Item.value - tempValue2);
            }
        }
        else
        {

            InventoryObject temp = items[item1.ID_Slot];
            items[item1.ID_Slot] = items[item2.ID_Slot];
            items[item2.ID_Slot] = temp;

        }
    }

    public void SwapItemEquip(SlotComponent item1, SlotComponent item2)
    {
        Debug.LogWarning("Equip");
        if (item1.typeSlot == SlotType.EquipSlot && item2.typeSlot == SlotType.InventSlot)
        {
            AddItemSlot( item1, item2, SlotType.InventSlot);


        }
        else if (item1.typeSlot == SlotType.InventSlot && item2.typeSlot == SlotType.EquipSlot)
        {
            AddItemSlot(item1, item2, SlotType.EquipSlot);

        }
    }

    public void SwapItems(SlotComponent item1, SlotComponent item2)
    {
        if (item1.ID_Slot != item2.ID_Slot)
        {
            bool first = item2.typeSlot == SlotType.EquipSlot || item1.typeSlot == SlotType.EquipSlot;
            if (first)
            {
                SwapItemEquip(item1, item2);
            }
            else
            {
                SwapItemSlot(item1, item2);
            }
        }
    }

    public void CheckDistance(GameObject currentObject, Vector3 localPosition, GameObject container)
    {
        bool canSwap = false;
        for (int i = 0; i < slots.Count; i++)
        {
            Vector2 center2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(container.GetComponent<RectTransform>(), slots[i].GetComponent<RectTransform>().position, null, out center2);
            float distance = Vector3.Distance(localPosition, center2);
            if (distance < 40)
            {
                Debug.Log("1");
                SwapItems(currentObject.GetComponent<SlotComponent>(), slots[i]);
                UpdateInventory();
                canSwap = true;
                break;
            }
        }
        if (!canSwap)
        {
            DropItem(currentObject);
        }
    }

    public void DropItem(GameObject currentObject)
    {
        for (int i = 0; i < currentObject.GetComponent<SlotComponent>().item.qantity_Item.value; i++)
        {
            GeneratorItemsInWorld.GenerateItem(currentObject.GetComponent<SlotComponent>().item.Item, Player.playerPosition);
        }   
        items[currentObject.GetComponent<SlotComponent>().ID_Slot] = new InventoryObject();
        currentObject.GetComponent<SlotComponent>().ResetSlot();
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (items[i].Item != null)
            {
                slots[i].GetComponent<SlotComponent>().DisplaySlot(items[i]);
            }
            else
            {
                slots[i].GetComponent<SlotComponent>().ResetSlot();
            }

            if (items[i].qantity_Item.ValueMoreTreshold())
            {
                items[i].slotStatus = Slot_Status.Filled;
            }
            else
            {
                items[i].slotStatus = Slot_Status.Unfilled;
            }
            if (items[i].Item == null)
            {
                items[i].slotStatus = Slot_Status.NoItem;
            }
        }
    }

    public void UpdateInventory(int i)
    {
       
            slots[i].DisplaySlot(items[i]);
        
    }

    public void CreateInventory()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].ID_Slot = i;
            items[i].slotStatus = Slot_Status.NoItem;
         //   items[i].typeSlot = slots[i].GetComponent<SlotComponent>().item.typeSlot;
        }
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ID_Slot = i;
            slots[i].controller = gameObject.GetComponent<Inventory_Controller>();
        }
    }

    public void InItem(GameObject item)
    {
        Debug.Log(takedItem.Count);
        takedItem.Add(item);
        takeButton.SetActive(true);
    }

    public void OutItem(GameObject item)
    {
        Debug.Log(takedItem.Count);
        if (takedItem.Count > 0 && item != null)
        {
            takedItem.RemoveAt(takedItem.Count-1);
        }
        if (takedItem.Count == 0) { 
            takeButton.SetActive(false);
        }
    }

}

