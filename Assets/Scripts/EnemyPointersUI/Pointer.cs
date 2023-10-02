using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class Pointer
    {
        private readonly IPointerMovement _pointerMovement;
        private readonly IPointerRotation _pointerRotation;
        private readonly Camera _camera;

        public Pointer(IPointerMovement pointerMovement, IPointerRotation pointerRotation, Camera camera)
        {
            _pointerMovement = pointerMovement;
            _pointerRotation = pointerRotation;
            _camera = camera;
        }

        public bool TryPoint(Vector3 pointFromPosition, Vector3 targetPosition)
        {
            Vector2 fromPosition = GetValidScreenPosition(pointFromPosition);
            Vector2 targetOnScreen = GetValidScreenPosition(targetPosition);
            Vector2 screenDirection = targetOnScreen - fromPosition;

            _pointerRotation.RotateToDirection(screenDirection);

            return _pointerMovement.TryMove(targetOnScreen, fromPosition);
        }

        public Vector2 GetValidScreenPosition(Vector3 targetPosition)
        {
            Vector3 targetOnScreen = _camera.WorldToScreenPoint(targetPosition);

            if (IsValidScreenPosition(targetOnScreen))
                targetOnScreen = new Vector2(-targetOnScreen.x, -targetOnScreen.y);

            return targetOnScreen;
        }
        
        public Vector2 GetViewportPosition(Vector3 targetPosition)
        {
            Vector3 targetOnScreen = _camera.WorldToViewportPoint(targetPosition);

            return targetOnScreen;
        }

        private bool IsValidScreenPosition(Vector3 targetOnScreen) =>
            targetOnScreen.z < 0;
    }
}