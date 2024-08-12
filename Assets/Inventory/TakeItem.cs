using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TakeItem : UIToolKitConnectable
{
    [SerializeField] private string _buttonTakeName = "TakeButton";
    [SerializeField] private float _rangeTakeItems;
    [SerializeField] private List<GameObject> _itemsInRange = new List<GameObject>();
    [SerializeField] private InventoryManager _inventory;
    [SerializeField] private UnityEvent _addItemEvent;
    private Button _buttonTake;
    private void Awake() => Connect();

    private void Start()
    {
        _buttonTake = _rootElement.Q<Button>(_buttonTakeName);
        _buttonTake.clicked += TakeItems;

    }
    private void Update()
    {

        CheckTriggerObjectsInRange(_rangeTakeItems);
        if (_itemsInRange.Count > 0)
            _buttonTake.style.display = DisplayStyle.Flex;
        else
            _buttonTake.style.display = DisplayStyle.None;
    }

    public void TakeItems()
    {
        if (_itemsInRange.Count > 0)
        {
            for(int i=0; i<_inventory.Items.Length;i++)
            {
                Item_Object itemTemp = _itemsInRange[0].GetComponent<RItem_Component>().item;
                if (_inventory.Items[i].slotStatus == Slot_Status.NoItem)
                {
                    AddNewItem(itemTemp, i);
                    return;
                }
                else if (_inventory.Items[i].slotStatus == Slot_Status.Unfilled)
                {
                    if (_inventory.Items[i].Item == itemTemp)
                    {
                        FoldItem(itemTemp, i);
                        return;
                    }
                }
            }
        }
    }

    private void AddNewItem(Item_Object itemTemp, int i)
    {
        _inventory.Items[i].Item = itemTemp;
        _inventory.Items[i].slotStatus = Slot_Status.Unfilled;
        _inventory.Items[i].qantity_Item = itemTemp.qantity_Item;
        if (_inventory.Items[i].qantity_Item.value == _inventory.Items[i].qantity_Item.Threshold)
            _inventory.Items[i].slotStatus = Slot_Status.Filled;
        Destroy(_itemsInRange[0]);
        _addItemEvent?.Invoke();
    }

    private void FoldItem(Item_Object itemTemp, int i)
    {
        _inventory.Items[i].qantity_Item.value += itemTemp.qantity_Item.value;
        if (_inventory.Items[i].qantity_Item.value == _inventory.Items[i].qantity_Item.Threshold)
            _inventory.Items[i].slotStatus = Slot_Status.Filled;
        Destroy(_itemsInRange[0]);
    }

    private void CheckTriggerObjectsInRange(float range)
    {
        _itemsInRange.Clear();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var collider in hitColliders)
        {
            if (collider.GetComponent<RItem_Component>() != null)
                _itemsInRange.Add(collider.gameObject);
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _rangeTakeItems);
    }
}
