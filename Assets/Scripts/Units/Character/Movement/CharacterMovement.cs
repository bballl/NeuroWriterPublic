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
        /// Движение персонажа.
        /// </summary>
        /// <returns>Направление движения по горизонтали.</returns>
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
        /// Логика горизонтального движения.
        /// </summary>
        /// <returns>Направление движения</returns>
        private float HoryzontalMove()
        {
            float direction = _inputController.GetHorizontal();

            if (Mathf.Abs(direction) < _threshold)
                return 0;

            _rb.velocity = new Vector2(direction * CharacterAttributes.Speed, _rb.velocity.y);

            return direction;
        }

        /// <summary>
        /// Логика вертикального движения.
        /// </summary>
        /// <returns>Направление движения</returns>
        private float VerticalMove()
        {
            float direction = _inputController.GetVertical();

            if (Mathf.Abs(direction) < _threshold)
                return 0;

            _rb.velocity = new Vector2(_rb.velocity.x, direction * CharacterAttributes.Speed);

            return direction;
        }

        /// <summary>
        /// Вращение оружия за курсром.
        /// </summary>
        private void WeaponRotate()
        {
            Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _weaponTransform.position;
            float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
            _weaponTransform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }
    }
}