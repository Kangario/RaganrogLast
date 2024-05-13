using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

[Serializable]
public struct UIGroup
{
    public UIDocument Document;
    public string Panel;
    public VisualElement RootElement;
    public VisualElement VisualElement;

    public VisualElement GetPanel()
    {
        RootElement = Document.rootVisualElement;
        VisualElement = RootElement.Q<VisualElement>(Panel);
        return VisualElement;
    }

}