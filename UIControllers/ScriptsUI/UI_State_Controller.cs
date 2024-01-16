using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_State_Controller : MonoBehaviour
{   
    [SerializeField] private StatTypeNeed typeBar;
    private Image image_Component;
    private void Start()
    {
        Charactres_Events.typeChange += ChangeBar;
        image_Component = GetComponent<Image>();
    }
    private void ChangeBar(StatTypeNeed type, LimitedNumber value)
    {

        if (type == typeBar)
        {
            image_Component.fillAmount = value.value/value.Threshold;
        }
    }
}
