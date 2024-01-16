using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events_UI : MonoBehaviour
{
    public static Action onCollisionBound;
    public static void CollisionUI()
    {
        onCollisionBound?.Invoke();
    }
}
