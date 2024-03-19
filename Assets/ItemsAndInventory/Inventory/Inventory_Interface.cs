using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemPicker
{
    public void CanTake(GameObject targetItem);
    public void TakeItem();
    public void AddItemInArray(int index_Slot);
}