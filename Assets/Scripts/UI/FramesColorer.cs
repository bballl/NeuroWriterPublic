using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class FramesColorer : MonoBehaviour
    {
        [SerializeField] private Image[] _frames;

        public void ColorFrames(Color color)
        {
            foreach (var frame in _frames)
            {
                frame.color = color;
            }
        }
    }
}