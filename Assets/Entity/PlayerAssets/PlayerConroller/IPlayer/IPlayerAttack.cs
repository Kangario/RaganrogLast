using System.Collections;
namespace RPlayer
{
    internal interface IPlayerAttack
    {
        public void Attack();
        public void ApplyDamage(float Damage);
        public IEnumerator AttackWait();
    }
}