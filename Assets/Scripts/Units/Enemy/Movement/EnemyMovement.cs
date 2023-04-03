using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        [SerializeField] protected EnemyModel _enemyModel;

        protected Transform _target;

        public abstract void Construct(EnemyData data);

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        protected bool IsTargetOnTheLeft(float xPointOfTarget)
        {
            if (xPointOfTarget < transform.position.x)
                return true;
            else
                return false;
        }

        public void StopMove()
        {
            _target = null;
        }
    }
}