using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Area_Group_Counter : MonoBehaviour
{
     public static List<GameObject> dropArea = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            dropArea.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
