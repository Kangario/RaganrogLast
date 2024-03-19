using UnityEngine;
[System.Serializable]
public struct LimitedNumber
{
    [SerializeField] public float value;
    [SerializeField] public float Threshold;
    public LimitedNumber(float Stat_value, float Stat_Threshold)
    {
        value = Stat_value;
        Threshold = Stat_Threshold;
    }
    public float LimitNumber()
    {
        if (value > Threshold)
        {
            value = Threshold;
            return value;
        }
        else
        {
            return value;
        }
    }
    public bool ValueMoreTreshold()
    {
        if (value == Threshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
