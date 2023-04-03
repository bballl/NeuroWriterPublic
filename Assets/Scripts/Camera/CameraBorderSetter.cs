using Cinemachine;
using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    class CameraBorderSetter : MonoBehaviour
    {
        [SerializeField] private CinemachineConfiner2D _confiner2D;

        private bool _isReady = false;

        private void Start()
        {
            StartCoroutine(WaitRoutine());
        }

        private IEnumerator WaitRoutine()
        {
            while (_isReady is false)
            {
                if (SceneContent.Instance == null)
                {
                    Debug.Log($"{name} waiting for SceneContent.Instance");
                    yield return new WaitForFixedUpdate();
                }
                else if (SceneContent.Instance.CreatedLocation == null)
                {
                    Debug.Log($"{name} waiting for SceneContent.Instance.CreatedLocation");
                    yield return new WaitForFixedUpdate();
                }

                _isReady = true;
            }

            Construct();
            yield break;
        }

        public void Construct()
        {
            _confiner2D.enabled = true;
            _confiner2D.m_BoundingShape2D = SceneContent.Instance.GetSceneLocation().CameraBorder;
        }
    }
}
