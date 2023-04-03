using UnityEngine;

namespace Assets.Scripts.Units.Character
{
    internal class CharacterAnimatorController
    {
        private Animator _animator;
        public CharacterAnimatorController(Animator animator) 
        {
            _animator = animator;
        }

        internal void ChangeAnimation(bool isMove)
        {
            _animator.SetBool("IsMove", isMove);
        }
    }
}
