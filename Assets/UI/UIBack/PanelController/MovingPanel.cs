using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class MovingPanel : UIToolKitConnectable
{
    [SerializeField] private string _groupUIName = "InventoryGroup";
    [SerializeField] private VisualElement _groupUI; 
    [SerializeField] private Vector2 _startPosition;

    private bool _isOpening;

    public bool IsOpening => _isOpening;

    private void Awake() => Connect();

    public void OpenPanel()
    {
        _groupUI =  _rootElement.Q<VisualElement>(_groupUIName);
        _groupUI.style.bottom = 0;
        _groupUI.style.left = 0;
        _isOpening = true;
    }
    public void ClosePanel()
    {
        _groupUI=  _rootElement.Q<VisualElement>(_groupUIName);
        _groupUI.style.bottom = _startPosition.y;
        _groupUI.style.left = _startPosition.x;
        _isOpening = false;
    }
}
