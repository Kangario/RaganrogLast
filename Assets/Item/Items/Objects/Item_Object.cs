using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/ItemBase")]
public class Item_Object : ScriptableObject
{   
    public Item_Object(Item_Object item) 
    {
        ID_Item = item.ID_Item;
        nam_Item = item.nam_Item;
        percent_Drop_Item= item.percent_Drop_Item;
        qantity_Item= item.qantity_Item;
    }

    public int ID_Item;
    public TypeItem type;
    public string nam_Item;
    public Sprite ico_Item;
    public float percent_Drop_Item;
    public LimitedNumber qantity_Item;
}