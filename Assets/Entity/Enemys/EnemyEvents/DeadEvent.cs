using System;
using UnityEngine;
public class DeadEvent : MonoBehaviour 
{
    public static event Action EnemyDead;
    public static event Action<Transform> EnemyDropItem;
    public static void OnEnemyDied(GameObject currentObject)
    {
        EnemyDead?.Invoke();
        Destroy(currentObject);
    }
    public static void onEnemyDropItem(Transform positionSpawn)
    {

        EnemyDropItem?.Invoke(positionSpawn);
    }
}
