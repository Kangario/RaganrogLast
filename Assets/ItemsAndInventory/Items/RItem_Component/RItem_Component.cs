using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RItem_Component : MonoBehaviour
{
    public Item_Object item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && gameObject.layer != 5)
        {
            Event_Items.OnItemTake(gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && gameObject.layer != 5)
        {
            Event_Items.OutFromObject(gameObject);
        }
    }
}
