using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class BlendRotation : IPointerRotation
    {
        private IPointerRotation _firstRotation;
        private IPointerRotation _secondRotation;
        private Transform _transform;
        
        private float _blendValue;

        public BlendRotation(IPointerRotation firstRotation, IPointerRotation secondRotation, Transform transform)
        {
            _transform = transform;
            _firstRotation = firstRotation;
            _secondRotation = secondRotation;
        }

        public void SetBlendValue(float value) => 
            _blendValue = Mathf.Clamp(value, 0, 1);

        public void RotateToDirection(Vector2 screenDirection)
        {
            _firstRotation.RotateToDirection(screenDirection);
            Quaternion firstRotation = _transform.rotation;
            
            _secondRotation.RotateToDirection(screenDirection);
            Quaternion secondRotation = _transform.rotation;

            _transform.rotation = Quaternion.Lerp(firstRotation, secondRotation, _blendValue);
        }
    }
}