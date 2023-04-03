using Cinemachine;
using UnityEngine;

namespace GameBoxProject
{
    public class CameraNoiser : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtCamera;
        [SerializeField] private float _shakeFrequency;

        private static float _shakeElepsedTime;
        private static float _activatedTime;
        private static float _activatedAmpl;
        private static CinemachineBasicMultiChannelPerlin _noise;
        
        private void Awake()
        {
            _noise = _virtCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            if (_shakeElepsedTime > 0)
            {
                _shakeElepsedTime -= Time.deltaTime;

                _noise.m_FrequencyGain = _shakeFrequency;
                _noise.m_AmplitudeGain = Mathf.Lerp(_activatedAmpl, 0, _shakeElepsedTime / _activatedTime);

                if (_shakeElepsedTime <= 0f)
                {
                    _noise.m_AmplitudeGain = 0f;
                    _shakeElepsedTime = 0f;
                }
            }
        }

        public static void Shake(ShakeType shakeType, float duration = 0.35f)
        {
            _activatedTime = duration;
            _noise.m_AmplitudeGain = (int)shakeType;

            _activatedAmpl = _noise.m_AmplitudeGain;
            _shakeElepsedTime = duration;
        }
    }

    public enum ShakeType
    {
        Low = 1,
        Medium = 2,
        Strong = 3
    }
}