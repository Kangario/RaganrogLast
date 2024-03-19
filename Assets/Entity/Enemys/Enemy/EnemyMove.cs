using System.Collections;
using UnityEngine;
namespace REnemy
{
    public class EnemyMove : IEnemy_Move
    {
        private Enemy_Object enemy_Type;
        private GameObject currentObject;
        private Rigidbody2D rb;
        private Animator animator;
        private Vector2 randomVector = Vector2.zero;

        public Vector2 GetRandomVector()
        {
            return randomVector;
        }
        public EnemyMove(Enemy_Object enemy_Type, GameObject currentObject)
        {
            this.enemy_Type = enemy_Type;
            this.currentObject = currentObject;
            rb = currentObject.GetComponent<Rigidbody2D>();
            animator = currentObject.GetComponent<Animator>();

        }
        public Vector2 FollowTarget(GameObject target)
        {
            Vector2 direction = target.transform.position + -currentObject.transform.position;
            return direction.normalized;
        }
        public void JumpMove(Vector2 direction)
        {
            float speed = enemy_Type.Speed_Enemy;
            Vector2 jumpDirection = new Vector2(direction.x, direction.y);
            rb.velocity = jumpDirection * speed;
            direction = direction.normalized;
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
        }
        public IEnumerator GenerateVectorEvery3Seconds()
        {
            while (true)
            {
                randomVector.x = Random.Range(-1f, 1f);
                randomVector.y = Random.Range(-1f, 1f);
                yield return new WaitForSeconds(3f);
            }
        }
    }
}