using System.Collections;
public interface IPlayer_Move
{
    public void ControlPlayer();
}
public interface IPlayer_Stats
{
    public void ChangeStat(StatTypeNeed typeStat, float value);
}
public interface IPlayer_Attack
{
    public void Attack();
    public void ApplyDamage(float Damage);
    public IEnumerator AttackWait();
}