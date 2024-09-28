using System.Collections;
using System.Linq;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Character_Object character_Preset;
    private SpriteRenderer spriteRenderer;
    private SortingLayerController sortLayer = new SortingLayerController();
    private Player_Move player_Move;
    private Player_Stats player_Stats;
    private Player_Attack player_Attack;
    private Coroutine start_Attack;
    public Player_Attack ReadOnly_Player_Attacking { get { return player_Attack; } private set { } }
    void Start()
    {
        gameObject.name = character_Preset.name_Player + character_Preset.ID_Player.ToString();
        player_Stats = new Player_Stats(character_Preset);
        player_Move = new Player_Move(gameObject, player_Stats);
        player_Attack = new Player_Attack(gameObject, player_Stats, character_Preset);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Charactres_Events.StartCoroutinAttack += StartCourutins;
        Charactres_Events.StopCoroutinAttack += StopCourutins;
        Charactres_Events.setExperience += player_Stats.GetExpirienceUp;
    }
    void FixedUpdate()
    {
        spriteRenderer.sortingOrder = sortLayer.LayerOrderController(gameObject);
        player_Move.ControlPlayer();
    }
    private void StartCourutins()
    {   if (player_Attack.WorkCoroutin == false)
        {
            start_Attack = StartCoroutine(player_Attack.AttackWait());
            player_Attack.WorkCoroutin = true;
            player_Stats.Player_Speed = 0;
        }
    }
    private void StopCourutins()
    {
        if (player_Attack.WorkCoroutin == true)
        {
            StopCoroutine(start_Attack);
            player_Attack.WorkCoroutin = false;
            player_Stats.Player_Speed = character_Preset.speed_Player;
        }
    }
}
public class Player_Move : IPlayer_Move 
{
    private Player_Stats character_Stat;
    private Joystic joystic_Move;
    private Rigidbody2D rb;
    private Animator animator;
    public Player_Move(GameObject currentObject, Player_Stats character_Stat)
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
public class Player_Attack : IPlayer_Attack
{
    private GameObject currentObject;
    private Player_Stats player_Stats;
    private Joystic joystic_Attack;
    private Character_Object character_Preset;
    private Animator animator;
    private bool workCoroutin = false;
    public bool WorkCoroutin { 
        get {
            animator.SetBool("IsAttack", workCoroutin);
            animator.speed = animator.GetCurrentAnimatorStateInfo(0).length / character_Preset.interval_Attack_Player;
            return workCoroutin; }
        set
        {
            animator.SetBool("IsAttack", value);
            workCoroutin = value;
            animator.speed = 1;
        }
    }
    public Player_Attack(GameObject currentObject, Player_Stats player_Stats, Character_Object character_Preset)
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
        directionAttack = directionAttack.normalized/7;
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
            player_Stats.ChangeStat(StatTypeNeed.Health, -Damage);
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
            Attack();
            yield return new WaitForSeconds(character_Preset.interval_Attack_Player);
        }
    }
}
public class Player_Stats : IPlayer_Stats
{
    private LimitedNumber health ;
    private LimitedNumber mana;
    private LimitedNumber stamina;
    private Level level;
    //----------------------
    private float regen_health;
    private float regen_mana;
    private float regen_stamina;
    //------------------------
    private float player_Speed;
    public Player_Stats(Character_Object character_Preset)
    {
        health = character_Preset.health_Player;
        mana = character_Preset.mana_Player;
        stamina = character_Preset.stamina_Player;
        level = character_Preset.level_Player;
        regen_health = character_Preset.regen_Health_Player;
        regen_mana = character_Preset.regen_Mana_Player;
        regen_stamina = character_Preset.regen_Stamina_Player;
        player_Speed = character_Preset.speed_Player;
    }
    public LimitedNumber  Health{ get { return health; } set { if (value.value < 0) health.value = 0; else health = value; } }
    public LimitedNumber Mana { get { return mana; } set { if (value.value < 0) mana.value = 0; else mana = value; } }
    public LimitedNumber Stamina { get { return stamina; } set { if (value.value < 0) stamina.value = 0; else stamina = value; } }
    public Level Level { get { return level; } set { } }
    public float RegenHp { get { return regen_health; } set { if (value < 0) regen_health = 0; else regen_health = value; } }
    public float RegenMana { get { return regen_mana; } set { if (value < 0) regen_mana = 0; else regen_mana = value; } }
    public float RegenStamina { get { return regen_stamina; } set { if (value < 0) regen_stamina= 0; else regen_stamina= value; } }
    public float Player_Speed { get { return player_Speed; } set { if (player_Speed >= 0) player_Speed = value; else  player_Speed = 0;  } }
    public void GetExpirienceUp(Level level_Mob)
    {
        level.experience += level_Mob.experience_threshold;
        if (level.experience >= level.experience_threshold)
        {
            LevelUp(level);
        }
        Charactres_Events.GetExperience(level); 
    }
    public void LevelUp(Level level)
    {
        level.level++;
        health.Threshold += 10;
        mana.Threshold += 10;
        stamina.Threshold += 10;
        level.experience_threshold += level.experience_threshold + Random.Range(0,level.experience_threshold);
        level.experience = 0;
        Debug.LogWarning(level.level + " ”ра левел ап");
    }
    
    public void ChangeStat(StatTypeNeed typeStat, float value)
    {
        switch (typeStat)
        {
            case StatTypeNeed.Health:
                health.value += value;
                Charactres_Events.ChangeStat(StatTypeNeed.Health, health);
            break;
            case StatTypeNeed.Mana:
                mana.value += value;
                Charactres_Events.ChangeStat(StatTypeNeed.Mana, mana);
                break;
            case StatTypeNeed.Stamina:
                stamina.value += value;
                Charactres_Events.ChangeStat(StatTypeNeed.Stamina, stamina);
            break;
        }
    }
}

