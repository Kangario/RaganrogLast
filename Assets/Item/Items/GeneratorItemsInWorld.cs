using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratorItemsInWorld : MonoBehaviour
{
    public Item_Object[] items;
    private void Start()
    {
        DeadEvent.EnemyDropItem += RandomizeItems;
    }

    public void RandomizeItems(Transform positionSpawn)
    {
        int random_Item_Index = Random.Range(0, items.Length);
        GenerateItem(items[random_Item_Index],positionSpawn);
    }
    public static void GenerateItem(Item_Object item, Transform positionSpawn)
    {
        GameObject tempItem = new GameObject(item.nam_Item);
        tempItem.AddComponent<RItem_Component>();
        tempItem.GetComponent<RItem_Component>().item = item;
        tempItem.AddComponent<SpriteRenderer>();
        tempItem.GetComponent<SpriteRenderer>().sprite = item.ico_Item;
        tempItem.AddComponent<BoxCollider2D>();
        tempItem.GetComponent<BoxCollider2D>().isTrigger = true;
        tempItem.transform.position = new Vector3(positionSpawn.position.x, positionSpawn.position.y, 0);
        tempItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        tempItem.GetComponent<SpriteRenderer>().sortingOrder = SortingLayerController.LayerOrderController(tempItem) -1 ;
    }
    public static void GenerateItem(Weapon weaponItem,Transform positionSpawn)
    {
        GameObject itemWeapon = new GameObject(weaponItem.name);
        itemWeapon.AddComponent<Weapon_Component>();
        itemWeapon.GetComponent<Weapon_Component>().weapon = weaponItem;
        itemWeapon.AddComponent<SpriteRenderer>();
        itemWeapon.GetComponent<SpriteRenderer>().sprite = weaponItem.ico_Item;
        itemWeapon.AddComponent<BoxCollider2D>();
        itemWeapon.GetComponent<BoxCollider2D>().isTrigger = true;
        itemWeapon.transform.position = new Vector3(positionSpawn.position.x, positionSpawn.position.y, 0);
        itemWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        itemWeapon.GetComponent<SpriteRenderer>().sortingOrder = SortingLayerController.LayerOrderController(itemWeapon) -1;
    }
    public static void GenerateItem(Distant distantItem, Transform positionSpawn)
    {
        GameObject itemDistant = new GameObject(distantItem.name);
        itemDistant.AddComponent<RDistatnt_Component>();
        itemDistant.GetComponent<RDistatnt_Component>().distant = distantItem;
        itemDistant.AddComponent<SpriteRenderer>();
        itemDistant.GetComponent<SpriteRenderer>().sprite = distantItem.ico_Item;
        itemDistant.AddComponent<BoxCollider2D>();
        itemDistant.GetComponent<BoxCollider2D>().isTrigger = true;
        itemDistant.transform.position = new Vector3(positionSpawn.position.x, positionSpawn.position.y, 0);
        itemDistant.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        itemDistant.GetComponent<SpriteRenderer>().sortingOrder = SortingLayerController.LayerOrderController(itemDistant) - 1;
    }
}
