using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumForLandscape;

[CreateAssetMenu(fileName = "NewlandscapeElements", menuName = "Landscape/CreateLandscapeElements")]
public class landscape_Elements : ScriptableObject
{
    public classLandscape nameObject;
    public Sprite spriteObject;
    public float rangeHeightMin;
    public float rangeHeightMax;
    public int id;

}
