using UnityEngine;

namespace Assets.Scripts.Units.Character.Movement
{
    /// <summary>
    /// Класс контролирует поворот оружия в направлении курсора мышки.
    /// </summary>
    internal sealed class CharacterWeaponRotation //не используется
    {
        private enum Side
        {
            Left = -1,
            Right = 1
        }

        private Vector2 _one;
        private Vector2 _two;
        private Transform _weaponTransform;
        private Camera _camera;

        internal CharacterWeaponRotation(Transform transform)
        {
            _weaponTransform = transform;
            _one = Vector2.right;
            _camera = Camera.main;
        }
        public void Move()
        {
            float z = GetValueZ();
            _weaponTransform.rotation = Quaternion.Euler(0, 0, z);
        }

        private float GetValueZ()
        {
            _two = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - _weaponTransform.position;
            float scalarComposition = _one.x * _two.x + _one.y * _two.y;
            float mudelesComposition = _one.magnitude * _two.magnitude;
            float division = scalarComposition / mudelesComposition;
            float angle = Mathf.Acos(division) * Mathf.Rad2Deg * (int)GetSide();
            return angle;
        }

        private Side GetSide()
        {
            Side side = Side.Right;
            if (_two.y <= _one.y)
                side = Side.Left;
            return side;
        }

        //private void OnDrawGizmos()
        //{
        //    if (_weaponTransform != null)
        //    {
        //        Gizmos.DrawLine(_weaponTransform.position, _one * 10);
        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawLine(_weaponTransform.position, _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition));

        //    }
        //}
    }
}
