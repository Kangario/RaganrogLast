using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System.Xml.Linq;
using Unity.VisualScripting;


public class HoverEffetct : UIToolKitConnectable
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color _hoverColor;
    [Space]
    [SerializeField] private List<string> _elementNames;
    private List<Button> _elements = new List<Button>();
    private void Awake() => Connect();

    private void Start()
    {
        foreach (string element in _elementNames) {
            _elements.Add(_rootElement.Q<Button>(element));   
        }
        foreach (Button button in _elements)
        {
            button.RegisterCallback<MouseEnterEvent>(evt => OnMouseEnter(evt, button));
            button.RegisterCallback<MouseLeaveEvent>(evt => OnMouseLeave(evt, button));
        }


    }

    private void OnMouseEnter(MouseEnterEvent evt, Button button)
    {
        button.style.unityBackgroundImageTintColor = _hoverColor;
    }

    private void OnMouseLeave(MouseLeaveEvent evt, Button button)
    {
        button.style.unityBackgroundImageTintColor = defaultColor;
    }
}
