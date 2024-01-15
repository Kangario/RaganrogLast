using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LimitedNumber
{   
    [SerializeField] public float value;
    [SerializeField] public float Threshold;
    public LimitedNumber(float Stat_value, float Stat_Threshold)
    {
        value = Stat_value;
        Threshold = Stat_Threshold;
    }
    public float LimitNumber()
    {
        if (value > Threshold)
        {
            value = Threshold;
            return value;
        }
        else
        {
            return value;
        }
    } 
    public bool ValueMoreTreshold()
    {
        if (value >= Threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public struct Level
{
    [SerializeField] public float experience;
    [SerializeField] public float experience_threshold;
    [SerializeField] public int level;
    public Level(float Experience, float Level_threshold, int Level)
    {
        experience = Experience;
        experience_threshold = Level_threshold;
        level = Level;
   
   }
}
[System.Serializable]
public struct InventObject
{
    [SerializeField] public Item_Object item_Setting;
    [SerializeField] public Vector2 box_Collider_Size;
    [SerializeField] public LimitedNumber quantity_Item;
    [SerializeField] public LimitedNumber inInventory;

}
[System.Serializable]
public struct AnchoredPos
{
    [SerializeField] public float point1;
    [SerializeField] public float point2;
    [SerializeField] public float point3;
    [SerializeField] public float point4;
    public AnchoredPos(float point1, float point2, float point3, float point4) 
    { 
        this.point1 = point1;
        this.point2 = point2;
        this.point3 = point3;
        this.point4 = point4;
    }

}