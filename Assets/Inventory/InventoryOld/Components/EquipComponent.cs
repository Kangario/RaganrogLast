using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipComponent : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public int ID_Slot;
    public InventoryObject item;
    public Inventory_Controller controller;
    public SlotType type;
    private Image image;
    private Vector3 originalPosition;
    private Vector2 localPointerPosition;
    [SerializeField] private GameObject containe;

    private void Start()
    { 
        image = gameObject.GetComponent<Image>();
    }
    public void DisplaySlot(InventoryObject item)
    {
        this.item = item;
      //  image.sprite = item.Item.ico_Item;
        image.color = new Color(255, 255, 255, 255);
    }
    public void ResetSlot()
    {
        this.item = new InventoryObject();
        image.color = new Color(255, 255, 255, 0);
        image.sprite = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ���������� ��������� ������� ��� �������
        originalPosition = gameObject.transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���������� ������ �� ��������� ������� ��� ���������� ������
        gameObject.transform.position = originalPosition;
        controller.CheckDistance(gameObject, localPointerPosition, containe);


    }
    public void OnDrag(PointerEventData eventData)
    {

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(containe.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            GetComponent<RectTransform>().anchoredPosition = localPointerPosition;
        }
    }

}
