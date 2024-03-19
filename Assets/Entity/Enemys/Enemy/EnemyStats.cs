namespace REnemy
{
    public class EnemyStats : IEnemy_Stats
    {
        private LimitedNumber health;
        private LimitedNumber mana;
        private LimitedNumber stamina;
        private Level expirience_Drop;
        public LimitedNumber Health { get { return health; } set { health = value; } }
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

        public EnemyStats(Enemy_Object enemy_Type)
        {
            health = enemy_Type.Health_Enemy;
            mana = enemy_Type.Mana_Enemy;
            stamina = enemy_Type.Stamina_Enemy;
            expirience_Drop = enemy_Type.Experience_Drop_Enemy;
        }
        public void ChangeStats(PlayerStat type, float value)
        {
            switch (type)
            {
                case PlayerStat.Health:
                    health.value += value;
                    break;
                case PlayerStat.Mana:
                    mana.value += value;
                    break;
                case PlayerStat.Stamina:
                    stamina.value += value;
                    break;
            }
        }
    }
}