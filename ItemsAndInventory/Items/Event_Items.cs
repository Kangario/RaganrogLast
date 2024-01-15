using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Items : MonoBehaviour
{
    public static event Action<GameObject> item_Take;

    public static void OnItemTake(GameObject targetItem)
    {
        item_Take?.Invoke(targetItem);
    }
}
