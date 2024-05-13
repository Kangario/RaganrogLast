using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPlayer;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class InventoryManager : UIToolKitConnectable
{
    [SerializeField] private string _slotName = "Slot";
    private List<Button> _slots = new List<Button>();
    public InventoryObject[] Items;
    private Button _selectedSlot;
    public bool isClikced;
    
    private void Awake() => Connect();

    private void Start()
    {
        var labels = _rootElement.Query<Button>().Where(element => element.name == _slotName).ToList(); ;
        foreach (Button label in labels) {
            _slots.Add(label); 
        }
        for(int i = 0; i< _slots.Count; i++) 
        {
            InventoryObject newInventoryObject = new InventoryObject();
            newInventoryObject.Slot = _slots[i];
            newInventoryObject.ID_Slot = i;
            Items[i] = newInventoryObject;
            int localIndex = i;
            _slots[i].RegisterCallback<MouseMoveEvent>(DraggingSlots);
            _slots[i].RegisterCallback<ClickEvent>(ev => OnSlotDown(ev, localIndex));
            _slots[i].RegisterCallback<PointerUpEvent>(ev => OnSlotUp(ev, localIndex));
        
        }
    }

    private void Update()
    {
        foreach (InventoryObject item in Items) 
            if (item.Item != null)
                DisplaySlots(item);
    }
    private void DisplaySlots(InventoryObject item) 
    {
        item.Slot.style.backgroundImage = item.Item.ico_Item.texture;
        item.Slot.style.color = new Color(255, 255, 255, 255);
        item.Slot.text = item.qantity_Item.value.ToString();
    }

    private void OnSlotDown(ClickEvent evt, int index)
    {
        if (Items[index].Item != null)
        {
            _selectedSlot = _slots[index];
            isClikced = true;
            _selectedSlot.style.position = Position.Absolute;
        }
    }
    private void OnSlotUp(PointerUpEvent evt, int index)
    {
        if (Items[index].Item != null)
        {
            _selectedSlot = null;
            isClikced = false;
        }
    }
    private void DraggingSlots(MouseMoveEvent evt)
    {
        if (isClikced)
        {
            if (_selectedSlot != null)
            {
                Vector2 localPosition = _selectedSlot.parent.WorldToLocal(new Vector2(evt.mousePosition.x, evt.mousePosition.y));
                float centerX = _selectedSlot.resolvedStyle.width / 2;
                float centerY = _selectedSlot.resolvedStyle.height / 2;

                _selectedSlot.style.top = localPosition.y - centerY;
                _selectedSlot.style.left = localPosition.x - centerX;
            }
        }
    }

}
