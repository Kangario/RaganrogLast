using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RLandscape;

[CreateAssetMenu(fileName = "NewlandscapeElements", menuName = "Landscape/CreateLandscapeElements")]
public class LandscapeElements : ScriptableObject
{
    public classLandscape nameObject;
    public Sprite spriteObject;
    public float rangeHeightMin;
    public float rangeHeightMax;
    public int id;

}
