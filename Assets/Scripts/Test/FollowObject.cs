using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    public class FollowObject : MonoBehaviour
    {
        public float speed = 1f;
        public float distance = 3f;

        private Transform target;
        private Rigidbody2D rb;
        private Vector2 pointNearCharacter;
        public Transform IconTransform;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            if (target == null)
                target = FindObjectOfType<MainCharacterController>().transform;

            StartCoroutine(ChoosePoint());
        }

        private IEnumerator ChoosePoint()
        {
            pointNearCharacter = (Vector2)target.position + UnityEngine.Random.insideUnitCircle * distance;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1f));
            StartCoroutine(ChoosePoint());
            yield break;
        }

        void FixedUpdate()
        {
            if (pointNearCharacter == Vector2.zero)
                return;

            Vector2 direction = (pointNearCharacter - (Vector2)transform.position).normalized;
            rb.velocity = direction * speed;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                IconTransform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
                IconTransform.localScale = Vector3.one;
            }
        }
    }
}