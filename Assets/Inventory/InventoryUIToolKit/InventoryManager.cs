using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPlayer;
using UnityEngine.UIElements;
using RInventory;
using Unity.VisualScripting;

public class InventoryManager : UIToolKitConnectable
{
    [SerializeField] private string _parrentName = "ContainerForSlots";
    [SerializeField] private InventoryMover _moverSlots;
    public List<SlotContainer> Slots;
    public InventoryObject[] Items;
    public bool isClikced;
    private Scroller _containerForSlots;

    private void Awake() => Connect();

    private void Start()
    {
        _containerForSlots = _rootElement.Query<Scroller>(_parrentName);
        
    }

    private void Update()
    {

    }

    public void DisplaySlots() 
    {
        if (Items.Length > Slots.Count)
        {
            for(int i = Slots.Count; i <= Items.Length; i++)
            {
                AddNewSlot(Items[i].Item.ico_Item, Items[i].qantity_Item);
            }
        }
    }
    
    private void AddNewSlot(Sprite image, LimitedNumber quantity)
    {
        VisualElement slot = new VisualElement();
        slot.AddToClassList("styleSlot");
        VisualElement slotBack = new VisualElement();
        slotBack.AddToClassList("slotBack");
        Label slotQuantity = new Label();
        slotQuantity.AddToClassList("slotLabel");
        slot.Add(slotBack);
        slot.Add(slotQuantity);
        slotBack.style.backgroundImage = image.texture;
        slotQuantity.style.display = DisplayStyle.Flex;
        slotQuantity.text = quantity.value.ToString();
        _containerForSlots.Add(slot);
    }

}
