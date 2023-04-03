using UnityEngine;

namespace Assets.Scripts.Units.Character.Movement
{
    internal sealed class CharacterMovement
    {
        private Rigidbody2D _rb;
        private Transform _weaponTransform;
        private CharacterInputController _inputController;
        private float _threshold = 0.01f;

        internal CharacterMovement(Rigidbody2D rb, Transform weaponTransform)
        {
            _rb = rb;
            _weaponTransform = weaponTransform;
            _inputController = new CharacterInputController();
        }

        /// <summary>
        /// �������� ���������.
        /// </summary>
        /// <returns>����������� �������� �� �����������.</returns>
        public float Move()
        {
            WeaponRotate();
            
            float horizontalDirection = HoryzontalMove();
            float verticalDirection = VerticalMove();

            if (horizontalDirection == 0 && verticalDirection == 0)
                _rb.velocity = Vector2.zero;

            return horizontalDirection;
        }
        
        /// <summary>
        /// ������ ��������������� ��������.
        /// </summary>
        /// <returns>����������� ��������</returns>
        private float HoryzontalMove()
        {
            float direction = _inputController.GetHorizontal();

            if (Mathf.Abs(direction) < _threshold)
                return 0;

            _rb.velocity = new Vector2(direction * CharacterAttributes.Speed, _rb.velocity.y);

            return direction;
        }

        /// <summary>
        /// ������ ������������� ��������.
        /// </summary>
        /// <returns>����������� ��������</returns>
        private float VerticalMove()
        {
            float direction = _inputController.GetVertical();

            if (Mathf.Abs(direction) < _threshold)
                return 0;

            _rb.velocity = new Vector2(_rb.velocity.x, direction * CharacterAttributes.Speed);

            return direction;
        }

        /// <summary>
        /// �������� ������ �� �������.
        /// </summary>
        private void WeaponRotate()
        {
            Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _weaponTransform.position;
            float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
            _weaponTransform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }
    }
}