using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class EnemyWord : Enemy
    {
        private List<EnemyWordPart> _enemyLetters = new();

        private CommonEnemyData _dataByMinute;

        public static event Action<string, WordType> OnWordDead;

        private string _word;
        private WordType _type;

        internal override void Init(EnemyData data, CommonEnemyData dataByMinute)
        {
            _dataByMinute = dataByMinute;

            if (_textRendererParticleSystem == null)
                _textRendererParticleSystem = FindObjectOfType<TextRendererParticleSystem>();

            _data = data;

            Health = new Health(1f);
            Health.OnPersonDead += Dead;
        }

        public void SetWord(string word, WordType type)
        {
            _word = word;
            _type = type;
        }

        internal void AddLetter(EnemyWordPart letterEnemy)
        {
            if (_enemyLetters.Count >= 1)
            {
                letterEnemy.SetTargetToFollow(_enemyLetters.Last().transform);
                letterEnemy.transform.position = _enemyLetters.Last().transform.position + _enemyLetters.Last().transform.right;
            }

            letterEnemy.OnDamageTaken += TakeDamage;
            
            _enemyLetters.Add(letterEnemy);
        }

        public override void TakeDamage(float damage)
        {
            float lastHp = Health.Hp;

            float givenDamage = (lastHp > damage) ? damage : lastHp;

            if (givenDamage != 0)
            {
                float damagePart = damage / _enemyLetters.Count;
                float greenColor = (255f - Mathf.Clamp(givenDamage, 0f, 255f)) / 255f;
                Color color = new Color(1f, greenColor, 0f);

                foreach (var enemy in _enemyLetters)
                {
                    enemy.ShowTakeDamage();
                    _textRendererParticleSystem.SpawnParticle(enemy.transform.position, damagePart, color, 0.8f);
                }
            }

            Health.TakeDamage(givenDamage);
            //CameraNoiser.Shake(ShakeType.Low);
        }

        public override void SetTargetToFollow(Transform target)
        {
            if (_enemyLetters.Count > 0)
                _enemyLetters[0].SetTargetToFollow(target);
        }

        protected override void Dead(object obj)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    foreach (var enemy in _enemyLetters)
                    {
                        enemy.Dead();
                    }
                    OnWordDead?.Invoke(_word, _type);
                })
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    foreach (var enemy in _enemyLetters)
                    {
                        enemy.BackToPool();
                    }
                    Destroy(gameObject);
                })
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private void OnDestroy()
        {
            foreach (var enemy in _enemyLetters)
            {
                enemy.OnDamageTaken -= TakeDamage;
            }
        }

        internal void InitAttackAndHealth()
        {
            _enemyAttack?.Init(_dataByMinute.Damage);
            Health.SetValues(_dataByMinute.HP);
            ((WordAttack)_enemyAttack)?.SetHead(_enemyLetters[0].transform);
        }
    }
}
