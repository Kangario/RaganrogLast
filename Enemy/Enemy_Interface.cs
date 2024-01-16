using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEnemy_Stats
{
    void ChangeStats(StatTypeNeed type, float value);
}
public interface IEnemy_Move
{
    Vector2 GetRandomVector();
    Vector2 FollowTarget(GameObject target);
    void JumpMove(Vector2 direction);
    IEnumerator GenerateVectorEvery3Seconds();


}
public interface IEnemy_Attack
{
    void Attack(float AttackRange, float Damage);
    void ApplyDamage(float damage);
    IEnumerator AttackWait(float AttackRange, float Damage, float Interval_Attack_Enemy, GameObject target);
}
public interface IEnemy_Vision
{
    void SetVision();
}