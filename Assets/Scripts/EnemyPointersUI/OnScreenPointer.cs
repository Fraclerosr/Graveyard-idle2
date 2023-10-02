using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class OnScreenPointer : IPointerMovement
    {
        private readonly Transform _transform;

        public OnScreenPointer(Transform transform)
        {
            _transform = transform;
        }

        public bool TryMove(Vector2 targetOnScreen, Vector2 fromPosition)
        {
            _transform.position = targetOnScreen;

            return true;
        }
    }
}