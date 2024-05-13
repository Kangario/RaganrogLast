using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPlayer;
using UnityEngine.UIElements;


public class InventoryManager : UIToolKitConnectable
{
    [SerializeField] private string _slotName = "Slot";
    private List<Label> _slots = new List<Label>();
    public InventoryObject[] Items;

    private void Awake() => Connect();

    private void Start()
    {
        var labels = _rootElement.Query<Label>().Where(element => element.name == _slotName).ToList(); ;
        foreach (Label label in labels) {
            _slots.Add(label); 
        }
        for(int i =0; i< _slots.Count; i++) 
        {
            InventoryObject newInventoryObject = new InventoryObject();
            newInventoryObject.Slot = _slots[i];
            newInventoryObject.ID_Slot = i;
            Items[i] = newInventoryObject;
        }
    }

    private void DisplaySlots(InventoryObject item) 
    {
        item.Slot.style.backgroundImage = item.Item.ico_Item_Texture;
    }

}
