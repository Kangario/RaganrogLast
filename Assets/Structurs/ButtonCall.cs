using System;
using UnityEngine.Events;

[Serializable]
public struct ButtonCall
{
    public string ButtonName;
    public UnityEvent ButtonUnityEvent;
    public Action ButtonAction;

    public void Initialization()
    {
        ButtonAction += CallAction;
    }

    public void CallAction()
    {
        ButtonUnityEvent?.Invoke();
    }
}
[Serializable]
public struct ButtonCall<T>
{
    public string ButtonName;
    public UnityEvent<T> ButtonUnityEvent;
    public Action<T> ButtonAction;

    public void Initialization()
    {
        ButtonAction += CallAction;
    }

    public void CallAction(T argument)
    {
        ButtonUnityEvent?.Invoke(argument);
    }
}
