using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject movedUiDefoultPos;
    [SerializeField] private GameObject movedUi;
    [SerializeField] private GameObject targetUi;
    [SerializeField] private float speedMoved;
    bool isUiMoved = true;
    void Update()
    {
        
        if (!isUiMoved)
        {
            if (movedUi.transform.position.y >= targetUi.transform.position.y)
            {
                movedUi.transform.Translate(0, -Time.deltaTime * speedMoved, 0);
            }
        }
        else
        {
            if (movedUi.transform.position.y <= movedUiDefoultPos.transform.position.y)
            {
                movedUi.transform.Translate(0, Time.deltaTime * speedMoved, 0);
            }
        }
    }
    public void MoveUI()
    {
        if (!isUiMoved)
        {
           
            isUiMoved = true;

        }
        else
        {

            isUiMoved = false;
        }
    }
}
