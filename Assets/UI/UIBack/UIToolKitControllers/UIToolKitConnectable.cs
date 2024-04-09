using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolKitConnectable : UIToolKitConnector
{
    protected VisualElement _rootElement;

    protected bool Connect()
    {
       if (RootView != null)
        {
            _rootElement = RootView;
            return true;
        }
        else
            return false; 
    }
}
