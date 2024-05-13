using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ButtonConnector : UIToolKitConnectable
{
    public List<ButtonCall> buttons;

    public List<ButtonCall<string>> ButtonsType;

    private void Awake() => Connect();

    private void Start()
    {
        foreach(ButtonCall button in buttons)
        {
            button.Initialization();
            _rootElement.Q<Button>(button.ButtonName).clicked += button.ButtonAction;
        }
        foreach (ButtonCall<string> buttonType in ButtonsType)
        {
            buttonType.Initialization();
          //  _rootElement.Q<Button>(buttonType.ButtonName).clicked += buttonType.ButtonAction;
        }
    }
}
