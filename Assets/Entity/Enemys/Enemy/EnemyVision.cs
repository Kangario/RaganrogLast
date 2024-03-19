using UnityEngine;
namespace REnemy
{
    public class EnemyVision : IEnemy_Vision
    {
        private Enemy_Object enemy_Type;
        private GameObject currentObject;
        public EnemyVision(Enemy_Object enemy_Type, GameObject currentObject)
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
}