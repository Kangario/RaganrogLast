using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Charactres_Events : MonoBehaviour
{
    public static event Action StartCoroutinAttack;
    public static event Action StopCoroutinAttack;
    public static event Action<StatTypeNeed, LimitedNumber> typeChange;
    public static event Action<Level> setExperience;
    public static event Action<Level> getExperience;
    public static void AttackDragJoystick()
    {
        StartCoroutinAttack?.Invoke();
    }
    public static void StopAttackDragJoystick()
    {
        StopCoroutinAttack?.Invoke();
    }
    public static void ChangeStat(StatTypeNeed type, LimitedNumber value)
    {
        typeChange?.Invoke(type, value);
    }
    public static void SetExperience(Level value)
    {
        setExperience?.Invoke(value);
    }
    public static void GetExperience(Level value)
    {
        getExperience?.Invoke(value);
    }

}
