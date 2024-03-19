using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Counter : MonoBehaviour
{
    public static List<GameObject> equipSlots = new List<GameObject>();
    private void Awake()
    {
        GetSlots();
    }
    public void GetSlots()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            equipSlots.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
