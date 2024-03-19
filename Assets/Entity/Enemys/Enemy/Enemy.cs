using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPlayer;
namespace REnemy
{

    public class Enemy : MonoBehaviour ,IEnemy
    {
        [SerializeField] private Enemy_Object enemy_Type;
        private bool isMove = false;
        private GameObject target = null;
        private Coroutine AttackCoroutine = null;
        private Coroutine MoveRandom = null;
        private SpriteRenderer spriteRenderer;
        private EnemyMove enemy_Move;
        private EnemyAttack enemy_Attack;
        private EnemyVision enemy_Vision;
        private EnemyStats enemy_Stats;
        public EnemyAttack ReadOnly_Enemy_Attacking { get { return enemy_Attack; } private set { } }
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            enemy_Stats = new EnemyStats(enemy_Type);
            enemy_Move = new EnemyMove(enemy_Type, gameObject);
            enemy_Attack = new EnemyAttack(target, gameObject, enemy_Stats);
            enemy_Vision = new EnemyVision(enemy_Type, gameObject);
            MoveRandom = StartCoroutine(enemy_Move.GenerateVectorEvery3Seconds());
        }
        private void FixedUpdate()
        {
            if (isMove)
            {
                spriteRenderer.sortingOrder = SortingLayerController.LayerOrderController(gameObject);
                Vector2 direction = enemy_Move.FollowTarget(target);
                enemy_Move.JumpMove(direction);
            }
            else
            {
                spriteRenderer.sortingOrder = SortingLayerController.LayerOrderController(gameObject);
                Vector2 direction = enemy_Move.GetRandomVector().normalized;
                enemy_Move.JumpMove(direction);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
            {
                target = collision.gameObject;
                AttackCoroutine = StartCoroutine(enemy_Attack.AttackWait(enemy_Type.Radius_Attack_Enemy, enemy_Type.Damage_Enemy, enemy_Type.Interval_Attack_Enemy, target));
                StopCoroutine(MoveRandom);
                enemy_Vision.SetVision();
                isMove = true;

            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
            {
                StopCoroutine(AttackCoroutine);

                isMove = false;

            }
        }

    }

}