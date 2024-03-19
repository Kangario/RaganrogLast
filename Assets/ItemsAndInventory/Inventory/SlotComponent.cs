using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotComponent : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public int ID_Slot;
    public InventoryObject item;
    public Inventory_Controller controller;
    private GameObject quantityItemObject;
    public SlotType typeSlot;
    public TypeItem typeItem;
    private Image image;
    private Vector3 originalPosition;
    private Vector2 localPointerPosition;
    [SerializeField] private GameObject containe;
    
    private void Start()
    {
        if (typeSlot == SlotType.InventSlot)
        {
            quantityItemObject = transform.GetChild(0).gameObject;
        }
        image = gameObject.GetComponent<Image>();
    }
    public void DisplaySlot(InventoryObject item)
    {
        this.item = item;
        image.sprite = item.Item.ico_Item;
        image.color = new Color(255, 255, 255, 255);
        if (typeSlot == SlotType.InventSlot)
        {
            quantityItemObject.GetComponent<TMP_Text>().text = item.qantity_Item.value.ToString();
            quantityItemObject.SetActive(true);
        }
    }
    public void ResetSlot()
    {
        this.item = new InventoryObject();
        image.color = new Color(255, 255, 255, 0);
        image.sprite = null;
        if (typeSlot == SlotType.InventSlot)
        {
            quantityItemObject.SetActive(false);
        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // Запоминаем начальную позицию при нажатии
        originalPosition = gameObject.transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {   

        if (item.slotStatus != Slot_Status.NoItem)
        {
            // Возвращаем объект на начальную позицию при отпускании пальца
            gameObject.transform.position = originalPosition;
            controller.CheckDistance(gameObject, localPointerPosition, containe);
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (item.slotStatus != Slot_Status.NoItem)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(containe.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                GetComponent<RectTransform>().anchoredPosition = localPointerPosition;
            }
        }
    }
  
}
