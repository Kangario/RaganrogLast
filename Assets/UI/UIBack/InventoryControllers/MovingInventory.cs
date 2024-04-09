using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class MovingInventory : UIToolKitConnectable
{
    [SerializeField] private string _inventory = "Inventory";
    [SerializeField] private Vector2 _startPosition;
    private VisualElement _visualInventory;
    private bool _isOpening;
    public bool IsOpening => _isOpening;

    private void Awake() => Connect();

    private void Start()
    {
        _visualInventory = _rootElement.Q<VisualElement>(_inventory);
    }

    public void OpenInventory()
    {
        _visualInventory.style.bottom = 0;
        _isOpening = true;
    }
    public void CloseInventory()
    {
        _visualInventory.style.bottom = _startPosition.y;
        _isOpening = false;
    }
}
