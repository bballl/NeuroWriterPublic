using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class MouseParallax : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _depth = 1f;
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;
        [SerializeField] private RectTransform _rectTrans;

        private Vector2 _prevMousePos;

        private void Start()
        {
            _prevMousePos = Input.mousePosition;
        }

        private void Update()
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - _prevMousePos;
            Vector2 parallax = -mouseDelta * _speed;

            Vector2 newPosition = _rectTrans.anchoredPosition + parallax * _depth;

            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, _minY, _maxY);

            _rectTrans.anchoredPosition = newPosition;

            _prevMousePos = Input.mousePosition;
        }
    }
}