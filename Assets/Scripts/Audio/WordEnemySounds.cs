using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class WordEnemySounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _wordAttackSound;
        [SerializeField] private AudioClip _wordEnemyDead;

        private void Start()
        {
            WordAttack.OnWordStartAttack += OnWordStartAttack;
            //EnemyWord.OnWordDead += OnEnemyWordDead;
        }

        private void OnEnemyWordDead(string arg1, WordType arg2)
        {
            _audio.PlayOneShot(_wordEnemyDead);
        }

        private void OnWordStartAttack()
        {
            _audio.PlayOneShot(_wordAttackSound);
        }

        private void OnDestroy()
        {
            WordAttack.OnWordStartAttack -= OnWordStartAttack;
            //EnemyWord.OnWordDead -= OnEnemyWordDead;
        }
    }
}