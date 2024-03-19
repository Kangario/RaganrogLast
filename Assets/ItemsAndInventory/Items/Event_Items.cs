using System;
using UnityEngine;

public class Event_Items : MonoBehaviour
{
    public static event Action<GameObject> InItem;
    public static event Action<GameObject> OutItem;
    public static event Action<GameObject, GameObject> onMove;
    public static void OnItemTake(GameObject targetItem)
    {
        InItem?.Invoke(targetItem);
    }
    public static void OutFromObject(GameObject targetItem)
    {
        OutItem?.Invoke(targetItem);
    }
    public static void MoveSlot(GameObject firstSlot, GameObject secondSlot)
    {
        onMove?.Invoke(firstSlot, secondSlot);
    }
}
