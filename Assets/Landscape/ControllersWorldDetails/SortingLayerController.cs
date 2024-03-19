using UnityEngine;
public static class SortingLayerController 
{
    public static int LayerOrderController(GameObject RGameObject)
    {
        int sizeWorld = GameObject.Find("TransferData").GetComponent<DataTransfer>().sizeWorld;
        int orderLayer = -4;
        for (int y = sizeWorld; y > 0; y--)
        {
            if (RGameObject.transform.position.y > y * 0.32f)
            {
                RGameObject.GetComponent<SpriteRenderer>().sortingOrder = orderLayer;
                return orderLayer;
            }
            orderLayer++;
        }
        return 0;
    }
}
