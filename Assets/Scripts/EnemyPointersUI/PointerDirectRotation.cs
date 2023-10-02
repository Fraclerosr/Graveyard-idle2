using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class PointerDirectRotation : IPointerRotation
    {
        private readonly Transform _transform;

        public PointerDirectRotation(Transform transform)
        {
            _transform = transform;
        }

        public void RotateToDirection(Vector2 screenDirection)
        {
            Vector3 axis = Vector3.forward;
            float zRotationAngle = Vector3.SignedAngle(Vector3.right, screenDirection, axis);
            _transform.eulerAngles = new Vector3(0, 0, zRotationAngle);
        }
    }
}