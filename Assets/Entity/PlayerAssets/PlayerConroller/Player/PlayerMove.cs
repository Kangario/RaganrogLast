using UnityEngine;
using RController;
namespace RPlayer
{
    public class PlayerMove : IPlayerMove
    {
        private PlayerStats character_Stat;
        private Joystic joystic_Move;
        private Rigidbody2D rb;
        private Animator animator;
        public PlayerMove(GameObject currentObject, PlayerStats character_Stat)
        {
            joystic_Move = GameObject.FindWithTag("Controller_Move").GetComponent<Joystic>();
            rb = currentObject.GetComponent<Rigidbody2D>();
            this.character_Stat = character_Stat;
            animator = currentObject.GetComponent<Animator>();
        }
        public void ControlPlayer()
        {
            Vector2 direction = joystic_Move.inputDirection;
            rb.velocity = direction * character_Stat.Player_Speed;
            direction = direction.normalized;
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
        }
    }
}