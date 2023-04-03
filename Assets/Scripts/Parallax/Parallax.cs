using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] _layers;
    [SerializeField] private float _smoothing = 1f;

    private Transform _camera;
    private Vector2 _previousCamPos;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Start()
    {
        _previousCamPos = _camera.position;   
    }

    private void Update()
    {
        for (int i = 0; i < _layers.Length; i++)
        {
            float parallax = (_previousCamPos.x - _camera.position.x) * _layers[i].Speed;
            float _backTargetPosition = _layers[i].Back.position.x + parallax;

            Vector2 newBackPos = new Vector2(_backTargetPosition, _layers[i].Back.position.y);
            _layers[i].Back.position = newBackPos;
        }

        _previousCamPos = _camera.position;
    }
}

[System.Serializable]
public struct ParallaxLayer
{
    [field: SerializeField] public Transform Back { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
}