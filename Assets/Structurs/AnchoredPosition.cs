using UnityEngine;

[System.Serializable]
public struct AnchoredPos
{
    [SerializeField] public float point1;
    [SerializeField] public float point2;
    [SerializeField] public float point3;
    [SerializeField] public float point4;
    public AnchoredPos(float point1, float point2, float point3, float point4)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.point3 = point3;
        this.point4 = point4;
    }

}