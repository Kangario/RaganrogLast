using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/ItemBase")]
public class Item_Object : ScriptableObject
{
    public int ID_Item;
    public string nam_Item;
    public Sprite ico_Item;
    public float percent_Drop_Item;
    public LimitedNumber qantity_Item;
}