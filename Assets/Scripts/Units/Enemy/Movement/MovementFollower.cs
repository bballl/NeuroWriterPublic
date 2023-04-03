using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class MovementFollower : EnemyMovement
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _minDistance;

        private float _speed;
        private Vector2 _pointNearCharacter;

        private bool _isStopped;

        public override void Construct(EnemyData data)
        {
            _speed = data.MoveSpeed;

            StartCoroutine(ChoosePoint());
        }

        public void Stop()
        {
            _isStopped = true;
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void ContinueMove()
        {
            _isStopped = false;
        }

        private IEnumerator ChoosePoint()
        {
            if (_target != null)
            {
                _pointNearCharacter = (Vector2)_target.position + UnityEngine.Random.insideUnitCircle * _minDistance;

                if (IsTargetOnTheLeft(_pointNearCharacter.x))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    _enemyModel.LookAtLeft();
                }
                else
                {
                    transform.localScale = Vector3.one;
                    _enemyModel.LookAtRight();
                }
            }

            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
            StartCoroutine(ChoosePoint());
            yield break;
        }

        private void FixedUpdate()
        {
            if (_target == null)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }    

            if (_pointNearCharacter == Vector2.zero)
                return;

            if (_isStopped)
                return;

            Vector2 direction = (_pointNearCharacter - (Vector2)transform.position).normalized;
            _rigidbody2D.velocity = direction * _speed;
        }
    }
}