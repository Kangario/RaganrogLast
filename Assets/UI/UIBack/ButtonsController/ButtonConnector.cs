using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ButtonConnector : UIToolKitConnectable
{
    public List<ButtonCall> buttons;

    private void Awake() => Connect();

    private void Start()
    {
        foreach(ButtonCall button in buttons)
        {
            button.Initialization();
            _rootElement.Q<Button>(button.ButtonName).clicked += button.ButtonAction;
        }
    } 
}
