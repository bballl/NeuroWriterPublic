using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    class RoundMovement : EnemyMovement
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _minDistance;

        private float _speed;
        private Vector2 _pointNearCharacter;

        public override void Construct(EnemyData data)
        {
            _speed = data.MoveSpeed;
            StartCoroutine(ChoosePoint());
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

            yield return new WaitForSeconds(Random.Range(2f, 4f));
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

            Vector2 direction = (_pointNearCharacter - (Vector2)transform.position).normalized;
            _rigidbody2D.velocity = direction * _speed;
        }
    }
}
