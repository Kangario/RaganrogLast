using System.Collections;
using UnityEngine;
namespace REnemy
{
    public interface IEnemy_Move
    {
        Vector2 GetRandomVector();
        Vector2 FollowTarget(GameObject target);
        void JumpMove(Vector2 direction);
        IEnumerator GenerateVectorEvery3Seconds();


    }
}