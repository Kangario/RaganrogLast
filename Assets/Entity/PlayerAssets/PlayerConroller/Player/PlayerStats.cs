using UnityEngine;
namespace RPlayer
{
    public class PlayerStats : IPlayerStats
    {
        private LimitedNumber health;
        private LimitedNumber mana;
        private LimitedNumber stamina;
        private Level level;
        //----------------------
        private float regen_health;
        private float regen_mana;
        private float regen_stamina;
        //------------------------
        private float player_Speed;
        public PlayerStats(Character_Object character_Preset)
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
        public LimitedNumber Health { get { return health; } set { if (value.value < 0) health.value = 0; else health = value; } }
        public LimitedNumber Mana { get { return mana; } set { if (value.value < 0) mana.value = 0; else mana = value; } }
        public LimitedNumber Stamina { get { return stamina; } set { if (value.value < 0) stamina.value = 0; else stamina = value; } }
        public Level Level { get { return level; } set { } }
        public float RegenHp { get { return regen_health; } set { if (value < 0) regen_health = 0; else regen_health = value; } }
        public float RegenMana { get { return regen_mana; } set { if (value < 0) regen_mana = 0; else regen_mana = value; } }
        public float RegenStamina { get { return regen_stamina; } set { if (value < 0) regen_stamina = 0; else regen_stamina = value; } }
        public float Player_Speed { get { return player_Speed; } set { if (player_Speed >= 0) player_Speed = value; else player_Speed = 0; } }
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
            level.experience_threshold += level.experience_threshold + Random.Range(0, level.experience_threshold);
            level.experience = 0;
            Debug.LogWarning(level.level + " Ура левел ап");
        }

        public void ChangeStat(PlayerStat typeStat, float value)
        {
            switch (typeStat)
            {
                case PlayerStat.Health:
                    health.value += value;
                    Charactres_Events.ChangeStat(PlayerStat.Health, health);
                    break;
                case PlayerStat.Mana:
                    mana.value += value;
                    Charactres_Events.ChangeStat(PlayerStat.Mana, mana);
                    break;
                case PlayerStat.Stamina:
                    stamina.value += value;
                    Charactres_Events.ChangeStat(PlayerStat.Stamina, stamina);
                    break;
            }
        }
    }
}