
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class Weapon : Item_Object
{   
    public PlayerStat typeNeed_Weapon;
    public TypeWeapon type_Weapon;
    public float consumption_Weapon;
    public float damage_Weapon;
    public float attack_Range_Weapon;
    public float interval_Attack_Weapon;
    public Weapon(Weapon item) : base(item)
    {
        typeNeed_Weapon = item.typeNeed_Weapon;
        consumption_Weapon= item.consumption_Weapon;
        damage_Weapon= item.damage_Weapon;
        attack_Range_Weapon= item.attack_Range_Weapon;
        interval_Attack_Weapon= item.interval_Attack_Weapon;
    }
}