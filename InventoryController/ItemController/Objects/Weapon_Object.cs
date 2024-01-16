
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class Weapon : Item_Object
{
    public StatTypeNeed typeNeed_Weapon;
    public float consumption_Weapon;
    public float damage_Weapon;
    public float attack_Range_Weapon;
    public float interval_Attack_Weapon;
}