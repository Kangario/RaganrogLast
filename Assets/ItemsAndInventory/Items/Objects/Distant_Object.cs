
using UnityEngine;


[CreateAssetMenu(fileName = "Distant", menuName = "Items/Distant")]
public class Distant : Item_Object
{
    public PlayerStat typeNeed_Distant;
    public TypeWeapon type_Weapon;
    public float consumption_Distant;
    public int ammunition_Quantity_Distant;
    public float speed_Flight_Distant;
    public float attack_Range_Distant;
    public float interval_Attack_Distant;
    public Distant(Distant item) : base(item)
    {
        typeNeed_Distant = item.typeNeed_Distant;
        consumption_Distant = item.consumption_Distant;
        ammunition_Quantity_Distant = item.ammunition_Quantity_Distant;
        speed_Flight_Distant= item.speed_Flight_Distant;
        attack_Range_Distant= item.attack_Range_Distant;
        interval_Attack_Distant = item.interval_Attack_Distant;
    }
}

