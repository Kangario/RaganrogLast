using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public abstract class UIToolKitConnector : MonoBehaviour
{
    public UIDocument _document;
    protected VisualElement _rootView;

    public virtual UIDocument Document
    {
        get
        {
            _document ??= GetComponent<UIDocument>();
            return _document;
        }
    }

    public virtual VisualElement RootView
    {
        get
        {
            _rootView ??= Document.rootVisualElement;
            return _rootView;
        }
    }
}
