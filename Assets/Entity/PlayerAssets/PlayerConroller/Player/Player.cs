using System.Collections;
using System.Linq;
using UnityEngine;
namespace RPlayer
{
    public class Player : MonoBehaviour , IPlayer
    {
        [SerializeField] private Character_Object character_Preset;
        private SpriteRenderer spriteRenderer;
        private PlayerMove player_Move;
        private PlayerStats player_Stats;
        private PlayerAttack player_Attack;
        private Coroutine start_Attack;
        public PlayerAttack ReadOnly_Player_Attacking { get { return player_Attack; } private set { } }
        public static Transform playerPosition;
        void Start()
        {
            gameObject.name = character_Preset.name_Player + character_Preset.ID_Player.ToString();
            player_Stats = new PlayerStats(character_Preset);
            player_Move = new PlayerMove(gameObject, player_Stats);
            player_Attack = new PlayerAttack(gameObject, player_Stats, character_Preset);
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            Charactres_Events.StartCoroutinAttack += StartCourutins;
            Charactres_Events.StopCoroutinAttack += StopCourutins;
            Charactres_Events.setExperience += player_Stats.GetExpirienceUp;
        }
        void FixedUpdate()
        {
            spriteRenderer.sortingOrder = SortingLayerController.LayerOrderController(gameObject);
            player_Move.ControlPlayer();
            playerPosition = gameObject.transform;
        }
        public void StartCourutins()
        {
            if (player_Attack.WorkCoroutin == false)
            {
                start_Attack = StartCoroutine(player_Attack.AttackWait());
                player_Attack.WorkCoroutin = true;
                player_Stats.Player_Speed = 0;
            }
        }
        public void StopCourutins()
        {
            if (player_Attack.WorkCoroutin == true)
            {
                StopCoroutine(start_Attack);
                player_Attack.WorkCoroutin = false;
                player_Stats.Player_Speed = character_Preset.speed_Player;
            }
        }
    }
}



