using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Inventory_Object_Component : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public InventObject invent_Object;
    private Image image;
    [SerializeField] private GameObject inventory_Slots_Group;
    private Drag dragAndDrop;
    private void Start()
    {
        dragAndDrop = new Drag(gameObject,inventory_Slots_Group.GetComponent<RectTransform>());       
        invent_Object = new InventObject();
        image = GetComponent<Image>(); 
    }
    public void ItemDisplayReset()
    {
        if (invent_Object.item_Setting != null)
        {
            image.sprite = null;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            GameObject text = gameObject.transform.GetChild(0).gameObject;
            text.SetActive(false);
            invent_Object = new InventObject(); 
        }
    }
        public void ItemDisplay()
    {
        if (invent_Object.item_Setting != null)
        {
            image.sprite = invent_Object.item_Setting.ico_Item;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
            if (invent_Object.quantity_Item.value > 1)
            {
                GameObject text = gameObject.transform.GetChild(0).gameObject;
            TextScaler(text);
            text.SetActive(true);
            
            text.GetComponent<Text>().text = invent_Object.quantity_Item.value.ToString();
            }
            
        }
    }
    public void TextScaler(GameObject text)
    {
        text.AddComponent<Text>();
        float scaleFactorWidth = Screen.width / 1920f;
        float scaleFactorHeight = Screen.height / 1080f;
        float scaleFactor = Mathf.Min(scaleFactorWidth, scaleFactorHeight);
        float scaledFontSize = Mathf.Clamp(text.GetComponent<Text>().fontSize * scaleFactor, 0, 150);
        text.GetComponent<Text>().fontSize = Mathf.RoundToInt(scaledFontSize*1.5f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        dragAndDrop.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragAndDrop.OnPointerUp(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragAndDrop.OnDrag(eventData);
    }

}

public class Drag : IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private GameObject current_Object;
    private RectTransform group_Object;
    private bool isDragging;
    private bool inTheSlot;
    private GameObject collision_Object;
    private Vector2 originalPosition;
    private Vector2 localMousePos;
    private float current_index;
    private float collision_index;
    private Drop dropItem;
    public Drag(GameObject current_Object, RectTransform group_Object)
    {
        this.current_Object = current_Object;
        this.group_Object = group_Object;
        originalPosition = current_Object.GetComponent<RectTransform>().anchoredPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (current_Object.GetComponent<Inventory_Object_Component>().invent_Object.item_Setting != null)
        {
            CheckCollision();
        }
        Debug.LogWarning(current_Object.GetComponent<RectTransform>() + "|" + inTheSlot + "|" + collision_Object);
        isDragging = false;
        current_Object.GetComponent<RectTransform>().anchoredPosition = originalPosition;
        if (inTheSlot)
        {
            if (collision_Object != null)
            {
                collision_Object.GetComponent<Inventory_Object_Component>().invent_Object = current_Object.GetComponent<Inventory_Object_Component>().invent_Object;
                collision_Object.GetComponent<Inventory_Object_Component>().ItemDisplay();
                current_Object.GetComponent<Inventory_Object_Component>().ItemDisplayReset();
               //Пофиксить это GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Inventory_Controller>().SwapItems(0,9);
                
            }
            inTheSlot = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(group_Object, mousePosition, Camera.main, out localMousePos);
            current_Object.GetComponent<RectTransform>().anchoredPosition = localMousePos;
        }
    }

    public void CheckCollision()
    {
        GameObject[] slots = Slot_Counter.slots.ToArray();
        GameObject[] dropSlots = Drop_Area_Group_Counter.dropArea.ToArray();
        Vector2 pointCurrent = new Vector2(current_Object.transform.position.x, current_Object.transform.position.y );
        for (int i = 0; i < slots.Length; i++)
        {
            if (IsPointInArea(slots[i].transform.position,pointCurrent,0.5f) &&slots[i].transform.position != current_Object.transform.position)
            {
                inTheSlot = true;
                collision_Object = slots[i];
                collision_index = i;
                //Уже начал но только с перетаскиванием
                break;
            }
            if (slots[i].transform.position == current_Object.transform.position)
            {
                current_index = i;
            }
        }
        for (int i = 0; i <dropSlots.Length; i++)
        {
            if (IsPointInArea(dropSlots[i].transform.position, pointCurrent, 4f))
            {
                dropItem = new Drop(current_Object);
                dropItem.DropItem();
                current_Object.GetComponent<Inventory_Object_Component>().ItemDisplayReset();
                break;
            }

        }
    }
    bool IsPointInArea(Vector2 point1, Vector2 point2, float areaSize)
    {
        float distance = Vector2.Distance(point1, point2);
        return distance <= areaSize;
    }

}
public class Drop
{
    Inventory_Object_Component currentObject;
    Item_Object item_Setting;
    public Drop(GameObject currentObject)
    {
        this.currentObject = currentObject.GetComponent<Inventory_Object_Component>();
        
    }

    public void DropItem()
    {
        item_Setting = currentObject.invent_Object.item_Setting;
        GameObject[] main_Hero = GameObject.FindGameObjectsWithTag("Player");
        GeneratorItemsInWorld.GenerateItem(item_Setting, main_Hero[0].transform);
    }


}