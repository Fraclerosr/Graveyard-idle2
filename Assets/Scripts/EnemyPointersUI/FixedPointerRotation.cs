using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class FixedPointerRotation : IPointerRotation
    {
        private readonly Quaternion _quaternion;
        private readonly Transform _transform;

        public FixedPointerRotation(Transform transform, Quaternion quaternion)
        {
            _transform = transform;
            _quaternion = quaternion;
        }

        public void RotateToDirection(Vector2 screenDirection)
        {
            _transform.rotation = _quaternion;
        }
    }
}