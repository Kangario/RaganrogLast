using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumForLandscape;


    [CreateAssetMenu(fileName = "NewlandscapeDecor", menuName = "Landscape/CreateLandscapeElementsDecor")] 
public class Landscape_Elements_Decor : ScriptableObject
    {
        public classDecor nameObject;
        public Sprite spriteObject;
        public float rangeHeightMin;
        public float rangeHeightMax;
        public int id;
   }

