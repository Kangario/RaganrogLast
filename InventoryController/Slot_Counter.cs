using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Counter : MonoBehaviour
{
    public static List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        GetSlots();
    }
    public void GetSlots()
    {
        for (int i=0; i<gameObject.transform.childCount; i++)
        {
            slots.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
