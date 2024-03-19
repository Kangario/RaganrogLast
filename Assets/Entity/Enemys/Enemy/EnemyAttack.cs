using System.Collections;
using UnityEngine;
using RPlayer;
namespace REnemy
{
    public class EnemyAttack : IEnemy_Attack
    {
        private EnemyStats enemy_Stats;
        private GameObject target;
        private GameObject currentObject;
        public EnemyAttack(GameObject target, GameObject currentObject, EnemyStats enemy_Stats)
        {
            this.target = target;
            this.currentObject = currentObject;
            this.enemy_Stats = enemy_Stats;
        }
        public void Attack(float AttackRange, float Damage)
        {
            float distants = Vector3.Distance(target.transform.position, currentObject.transform.position);

            if (distants <= AttackRange)
            {

                target.GetComponent<Player>().ReadOnly_Player_Attacking.ApplyDamage(Damage);
            }

        }
        public void ApplyDamage(float damage)
        {
            if (enemy_Stats.Health.value > 0)
            {
                enemy_Stats.ChangeStats(PlayerStat.Health, -damage);
            }
            else
            {
                DeadEvent.OnEnemyDied(currentObject);
                Charactres_Events.SetExperience(enemy_Stats.GetExpirience_Drop());
                DeadEvent.onEnemyDropItem(currentObject.transform);
                enemy_Stats.Health = new LimitedNumber(0, 10);
            }
        }
        public IEnumerator AttackWait(float AttackRange, float Damage, float Interval_Attack_Enemy, GameObject target)
        {
            while (true)
            {
                this.target = target;
                Attack(AttackRange, Damage);
                yield return new WaitForSeconds(Interval_Attack_Enemy);
            }
        }
    }
}