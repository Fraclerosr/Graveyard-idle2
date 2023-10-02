using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class PointerBehaviour : MonoBehaviour
    {
        private Pointer _onScreenPointer;
        private BlendRotation _pointerBlendRotation;
        private BlendPointer _pointerBlendMovement;

        public void Construct(RectTransform boundaries, Camera camera)
        {
            IPointerRotation pointerDirectRotation = new PointerDirectRotation(transform);
            IPointerRotation fixedPointerRotation = new FixedPointerRotation(transform, Quaternion.Euler(0, 0, -90));
            IPointerMovement onBoundariesPointer = new OnBoundariesPointer(boundaries, transform);
            IPointerMovement pointerMovement = new OnScreenPointer(transform);
            _pointerBlendRotation = new BlendRotation(fixedPointerRotation, pointerDirectRotation, transform);
            _pointerBlendMovement = new BlendPointer(pointerMovement, onBoundariesPointer, transform);
            _onScreenPointer = new Pointer(_pointerBlendMovement, _pointerBlendRotation, camera);
        }

        public void Point(Vector3 fromPosition, Vector3 targetPosition)
        {
            var blend = (Vector3.Distance(_onScreenPointer.GetViewportPosition(fromPosition),
                _onScreenPointer.GetViewportPosition(targetPosition)) - 0.4f) * 5f;

            blend = Mathf.Clamp(blend, 0, 1);

            _pointerBlendRotation.SetBlendValue(blend);
            _pointerBlendMovement.SetBlendValue(blend);
            _onScreenPointer.TryPoint(fromPosition, targetPosition);
        }
    }
}