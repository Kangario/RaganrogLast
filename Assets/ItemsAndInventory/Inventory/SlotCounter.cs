using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCounter : MonoBehaviour
{ 
    public static List<SlotComponent> slots = new List<SlotComponent>();

    private void Awake()
    {
        GetSlots();
    }

    public void GetSlots()
    {
        List<Transform> transforms = RMath.AddAllChildren(transform);
        foreach (Transform obj in transforms)
        {
            SlotComponent slot = obj.GetComponent<SlotComponent>();
            if (slot != null)
                slots.Add(slot);
        }
    }
    
}
