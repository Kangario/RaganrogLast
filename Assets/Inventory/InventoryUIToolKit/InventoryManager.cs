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
    private List<Button> _slots = new List<Button>();
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
        
    }
    
    private void AddNewSlot(Texture2D image, LimitedNumber quantity)
    {
        VisualElement slot = new VisualElement();
        slot.AddToClassList("styleSlot");
        VisualElement slotBack = new VisualElement();
        slotBack.AddToClassList("slotBack");
        Label slotQuantity = new Label();
        slotQuantity.AddToClassList("slotLabel");
        slot.Add(slotBack);
        slot.Add(slotQuantity);
        slotBack.style.backgroundImage = image;
        slotQuantity.style.display = DisplayStyle.Flex;
        slotQuantity.text = quantity.value.ToString();
        _containerForSlots.Add(slot);
    }

    private void OnSlotDown(ClickEvent evt, int index)
    {

        if (Items[index].Item != null)
        {
            _moverSlots.OnSlotDown();
            isClikced= true;
        }
    }

    private void OnSlotUp(PointerUpEvent evt, int index)
    {

        if (Items[index].Item != null)
        {
            _moverSlots.OnSlotUp(_slots[index]);
        }
    }

    private void DraggingSlots(MouseMoveEvent evt)
    {
        if (isClikced)
        {
            _moverSlots.SlotMove(evt);
        }
    }

}
