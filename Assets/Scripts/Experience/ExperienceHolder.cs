using UnityEngine;

namespace GameBoxProject
{
    class ExperienceHolder : MonoBehaviour
    {
        public Experience Experience { get; private set; }

        public float Multiplier { get; private set; }

        private void Awake()
        {
            Construct();
            Multiplier = 1f;
        }

        public void Construct()
        {
            Experience = new Experience();
        }

        public void SetMultiplier(float multiplier)
        { 
            Multiplier *= multiplier;
        }

        public void AddExp(int value)
        {
            var res = value * Multiplier;
            Experience.AddExperience((int)(Multiplier * value));
        }
    }
}
