using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Level_Controller : MonoBehaviour
{
    private Image image_Component;
    private void Start()
    {
        Charactres_Events.getExperience += ChangeBar;
        image_Component = GetComponent<Image>();
    }
    private void ChangeBar(Level value)
    {   

            image_Component.fillAmount = value.experience / value.experience_threshold;
        
    }

}
