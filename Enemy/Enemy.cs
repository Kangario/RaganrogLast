using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using static UnityEngine.GraphicsBuffer;



public class Enemy : MonoBehaviour 
{
    public Enemy_Object enemy_Type;
    private bool isMove = false;
    private GameObject target = null;
    private Coroutine AttackCoroutine = null;
    private Coroutine MoveRandom = null;
    private SpriteRenderer spriteRenderer;
    private SortingLayerController sortLayer = new SortingLayerController();
    private Enemy_Move enemy_Move;
    private Enemy_Attack enemy_Attack;
    private Enemy_Vision enemy_Vision;
    private Enemy_Stats enemy_Stats;
    public Enemy_Attack ReadOnly_Enemy_Attacking { get { return enemy_Attack; } private set { } }
    private void Start()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy_Stats = new Enemy_Stats(enemy_Type);
        enemy_Move = new Enemy_Move(enemy_Type, gameObject);
        enemy_Attack = new Enemy_Attack(target, gameObject, enemy_Stats);
        enemy_Vision = new Enemy_Vision(enemy_Type, gameObject);
        MoveRandom = StartCoroutine(enemy_Move.GenerateVectorEvery3Seconds());
    }
     private void FixedUpdate()
    {
        if (isMove)
        {
            spriteRenderer.sortingOrder =  sortLayer.LayerOrderController(gameObject);
            Vector2 direction = enemy_Move.FollowTarget(target);
            CheckMoveType(direction);
        }
        else
        {
            spriteRenderer.sortingOrder = sortLayer.LayerOrderController(gameObject);
            Vector2 direction = enemy_Move.GetRandomVector().normalized;
            CheckMoveType(direction);
        }

    }
    private void CheckMoveType(Vector2 direction)
    {
        switch (enemy_Type.Type_Move_Enemy)
        {
            case TypeEnemyMove.Jerk:
               //Будущий Тип Передвижения
                break;
            case TypeEnemyMove.Jump:
               enemy_Move.JumpMove(direction);
                break;
            case TypeEnemyMove.Run:
                //Будущий Тип Передвижения
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.layer == 3)
        {
            target = collision.gameObject;
            AttackCoroutine = StartCoroutine(enemy_Attack.AttackWait(enemy_Type.Radius_Attack_Enemy, enemy_Type.Damage_Enemy, enemy_Type.Interval_Attack_Enemy,target));
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


public class Enemy_Stats: IEnemy_Stats
{
    private LimitedNumber health;
    private LimitedNumber mana;
    private LimitedNumber stamina;
    private Level expirience_Drop;
    public LimitedNumber Health { get {return health; } set { health = value; } }
    public LimitedNumber Mana { get { return mana; } set { mana = value; } }
    public LimitedNumber Stamina { get { return stamina; } set { stamina = value; } }
    public Level GetExpirience_Drop()
    {
        return expirience_Drop;
    }
    public void SetExpirience_Drop(Level value)
    {
        expirience_Drop = value;
    }

    public Enemy_Stats(Enemy_Object enemy_Type) 
    {
        health = enemy_Type.Health_Enemy;
        mana = enemy_Type.Mana_Enemy;
        stamina = enemy_Type.Stamina_Enemy;
        expirience_Drop = enemy_Type.Experience_Drop_Enemy;
    }
    public void ChangeStats(StatTypeNeed type,float value)
    {
        switch (type)
        {
            case StatTypeNeed.Health:
                health.value += value; 
                break;
            case StatTypeNeed.Mana:
                mana.value += value;
                break;
            case StatTypeNeed.Stamina:
                stamina.value += value;
                break;
        }
    }
}
public class Enemy_Move: IEnemy_Move
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
    public Enemy_Move(Enemy_Object enemy_Type, GameObject currentObject)
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
public class Enemy_Attack : IEnemy_Attack
{
    private Enemy_Stats enemy_Stats;
    private GameObject target;
    private GameObject currentObject;
    public Enemy_Attack(GameObject target, GameObject currentObject, Enemy_Stats enemy_Stats)
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
            enemy_Stats.ChangeStats(StatTypeNeed.Health, -damage);
        }
        else
        {
            DeadEvent.OnEnemyDied(currentObject);
            Charactres_Events.SetExperience(enemy_Stats.GetExpirience_Drop());
            DeadEvent.onEnemyDropItem(currentObject.transform);
            enemy_Stats.Health =  new LimitedNumber(0,10);
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
public class Enemy_Vision:IEnemy_Vision
{
    private Enemy_Object enemy_Type;
    private GameObject currentObject;
    public Enemy_Vision(Enemy_Object enemy_Type, GameObject currentObject)
    {
        this.enemy_Type = enemy_Type;
        this.currentObject = currentObject; 
    } 
        public void SetVision()
    {
        Vector2[] fieldVision = enemy_Type.Vision_Field_Enemy;
        PolygonCollider2D poligons = currentObject.GetComponent<PolygonCollider2D>();
        poligons.isTrigger = true;
        poligons.SetPath(0, fieldVision);
    }
} 