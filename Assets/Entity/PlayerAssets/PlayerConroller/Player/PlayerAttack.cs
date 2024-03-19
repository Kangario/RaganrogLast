using System.Collections;
using System.Linq;
using UnityEngine;
using REnemy;
using RController;
namespace RPlayer
{
    public class PlayerAttack : IPlayerAttack
    {
        private GameObject currentObject;
        private PlayerStats player_Stats;
        private Joystic joystic_Attack;
        private Character_Object character_Preset;
        private Animator animator;
        private bool workCoroutin = false;
        public bool WorkCoroutin
        {
            get
            {
                animator.SetBool("IsAttack", workCoroutin);
                animator.speed = animator.GetCurrentAnimatorStateInfo(0).length / character_Preset.interval_Attack_Player;
                return workCoroutin;
            }
            set
            {
                animator.SetBool("IsAttack", value);
                workCoroutin = value;
                animator.speed = 1;
            }
        }
        public PlayerAttack(GameObject currentObject, PlayerStats player_Stats, Character_Object character_Preset)
        {
            joystic_Attack = GameObject.FindWithTag("Controller_Attack").GetComponent<Joystic>();
            animator = currentObject.GetComponent<Animator>();
            this.currentObject = currentObject;
            this.player_Stats = player_Stats;
            this.character_Preset = character_Preset;
        }

        public void Attack()
        {
            Vector2 currentPosition = new Vector2(currentObject.transform.position.x, currentObject.transform.position.y);
            Vector2 directionAttack = joystic_Attack.inputDirection.normalized * character_Preset.range_Attack_Player;
            directionAttack = directionAttack.normalized / 7;
            directionAttack += currentPosition;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(directionAttack, 0.06f);
            Collider2D[] boxCollider = colliders.Where(c => c is BoxCollider2D).ToArray();
            foreach (Collider2D colider in boxCollider)
            {
                if (colider.gameObject.layer == 6)
                {
                    colider.gameObject.GetComponent<Enemy>().ReadOnly_Enemy_Attacking.ApplyDamage(character_Preset.damage_Player);

                }
            }
            directionAttack = joystic_Attack.inputDirection.normalized;
            animator.SetFloat("XAttack", directionAttack.x);
            animator.SetFloat("YAttack", directionAttack.y);
        }
        public void ApplyDamage(float Damage)
        {
            if (player_Stats.Health.value > 0)
            {
                player_Stats.ChangeStat(PlayerStat.Health, -Damage);
            }
            else
            {
                DeadEvent.OnEnemyDied(currentObject);
            }
        }
        public IEnumerator AttackWait()
        {
            while (true)
            {

                yield return new WaitForSeconds(character_Preset.interval_Attack_Player / 2);
                Attack();
                yield return new WaitForSeconds(character_Preset.interval_Attack_Player / 2);
            }
        }
    }
}