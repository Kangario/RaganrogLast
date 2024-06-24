using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacter", menuName = "Characters/CharacterBase")]
public class Character_Object : ScriptableObject
{
    public int ID_Player = 0;
    public string name_Player = "Alex";
    public Sprite default_Sprite_Player;
    public RuntimeAnimatorController Animation_Controller_Player;
    [Header("Stat player")]
    public LimitedNumber health_Player ;
    public LimitedNumber mana_Player;
    public LimitedNumber stamina_Player;
    public Level level_Player;
    [Header("Regen stat")]
    public float regen_Health_Player = 1;
    public float regen_Mana_Player = 1;
    public float regen_Stamina_Player = 1;
    [Header("Buttle stat")]
    public float speed_Player = 1;
    public float damage_Player = 2;
    public float interval_Attack_Player = 1;
    public float range_Attack_Player = 1;
    public float range_Attack_Player_Circle = 0.06f;
}


