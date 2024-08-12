using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollingController : UIToolKitConnectable
{
    [SerializeField] private string _slotGroupName = "ContainerForSlots";
    [SerializeField] private string _slotBackName = "SlotBack";
    [SerializeField] private float _speedScroll = 1;
    private ScrollView _slotGroup;
    private bool isClicked;
    private bool isMoving;
    //private Vector3 _localDelta;

    private void Awake() => Connect();

    private void Start()
    {
        _slotGroup = _rootElement.Q<ScrollView>(_slotGroupName);
        VisualElement _slotBack = _rootElement.Q<VisualElement>(_slotBackName);
        //_rootElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
        //_rootElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
        
        //_rootElement.RegisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        Debug.LogWarning("Click");
        isClicked = true;
       // _localDelta = Vector2.zero;
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {

            isClicked = false;
            //_localDelta = Vector2.zero;
        }
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (isClicked)
        {
            Vector2 _localDelta  = evt.deltaPosition;
            Debug.LogWarning(_localDelta);
            _slotGroup.scrollOffset = new Vector2(0, _localDelta.y * _speedScroll);
        }
    }
}
