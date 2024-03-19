using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyBase")]
public class Enemy_Object : ScriptableObject
{
    public int ID_Enemy;
    public string Name_Enemy;
    public Sprite Image_Enemy;
    public RuntimeAnimatorController Animator_Enemy;
    public LimitedNumber Health_Enemy;
    public LimitedNumber Mana_Enemy;
    public LimitedNumber Stamina_Enemy;
    public Level Experience_Drop_Enemy;
    public float Damage_Enemy;
    public float Interval_Attack_Enemy;
    public float Speed_Enemy;
    public float Radius_Attack_Enemy;
    public Vector2[] Vision_Field_Enemy;
}

