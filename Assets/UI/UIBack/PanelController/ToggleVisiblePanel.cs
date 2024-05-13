using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToggleVisiblePanel : UIToolKitConnectable
{
    [SerializeField] private List<string> _panels;
    [SerializeField] private List<VisualElement> _panelElements = new List<VisualElement>();
    private int _currentIndexPanel;

    public int CurrentIndexPanel => _currentIndexPanel;

    private void Awake() => Connect();

    private void Start()
    {
        foreach (string panel in _panels) {
            _panelElements.Add(_rootElement.Q<VisualElement>(panel));
        }
        OpenPanel(0);
    }

    public void OpenPanel(int indexPanel)
    {
        for (int i =0; i< _panelElements.Count; i++)
        {
            
            _panelElements[i].style.display = DisplayStyle.None;
        }
        _currentIndexPanel = indexPanel;
        _panelElements[indexPanel].style.display = DisplayStyle.Flex;
    }
}
