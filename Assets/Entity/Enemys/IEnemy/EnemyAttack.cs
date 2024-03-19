using System.Collections;
using UnityEngine;
namespace REnemy
{
    internal interface IEnemy_Attack
    {
        void Attack(float AttackRange, float Damage);
        void ApplyDamage(float damage);
        IEnumerator AttackWait(float AttackRange, float Damage, float Interval_Attack_Enemy, GameObject target);
    }
}