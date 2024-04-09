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
