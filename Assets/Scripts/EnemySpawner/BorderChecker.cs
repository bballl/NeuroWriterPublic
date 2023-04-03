using UnityEngine;

namespace GameBoxProject
{
    public class BorderChecker : MonoBehaviour
    {
        [SerializeField] private Transform _bottomBorder;
        [SerializeField] private Transform _topBorder;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;

        [field: SerializeField] public PolygonCollider2D CameraBorder { get; private set; }

        public Vector2 GetRandomPosition()
        {
            float x = Random.Range(_leftBorder.position.x, _rightBorder.position.x);
            float y = Random.Range(_bottomBorder.position.y, _topBorder.position.y);

            return new Vector2(x, y);
        }

        public Vector2 GetRandomPositionNear(Vector2 position, float maxDistance)
        {
            while (true)
            {
                Vector2 result = position + Random.insideUnitCircle * maxDistance;
                if (CheckBorders(result) is false)
                    continue;

                return result;
            }
        }

        public Vector2 GetRandomOverCircle(Vector2 point, float radius)
        {
            while(true)
            {
                Vector2 result = point + Random.insideUnitCircle * radius;

                if (Vector2.Distance(point, result) < radius / 2)
                    continue;

                if (CheckBorders(result) is false)
                    continue;

                return result;
            }
        }

        private bool CheckBorders(Vector2 point)
        {
            if (point.x < _leftBorder.position.x)
                return false;

            if (point.x > _rightBorder.position.x)
                return false;

            if (point.y > _topBorder.position.y)
                return false;

            if (point.y < _bottomBorder.position.y)
                return false;

            return true;
        }
    }
}