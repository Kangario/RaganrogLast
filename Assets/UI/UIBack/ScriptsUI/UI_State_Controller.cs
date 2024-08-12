using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_State_Controller : UIToolKitConnectable
{   
    [SerializeField] private PlayerStat typeBar;
    [SerializeField] private string _stateBarName = "Health";
    private StatProgress _stat;

    private void Awake() => Connect();

    private void Start()
    {
        Charactres_Events.typeChange += ChangeBar;
        _stat = _rootElement.Q<StatProgress>(_stateBarName);
    }
    private void ChangeBar(PlayerStat type, LimitedNumber value)
    {

        if (type == typeBar)
        {
            _stat.Progress = value.value/value.Threshold;
        }
    }
}
