using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class BlendPointer : IPointerMovement
    {
        private readonly IPointerMovement _firstPointerMovement;
        private readonly IPointerMovement _secondMovement;
        private readonly Transform _transform;
        
        private float _blendValue;

        public BlendPointer(IPointerMovement firstPointerMovement, IPointerMovement secondMovement, Transform transform)
        {
            _transform = transform;
            _secondMovement = secondMovement;
            _firstPointerMovement = firstPointerMovement;
        }

        public bool TryMove(Vector2 targetOnScreen, Vector2 fromPosition)
        {
            bool blend = true;
            
            
            blend &= _firstPointerMovement.TryMove(targetOnScreen, fromPosition);
            Vector3 firstRotation = _transform.position;
            
            blend &= _secondMovement.TryMove(targetOnScreen, fromPosition);
            Vector3 secondRotation = _transform.position;

            if(blend)
                _transform.position = Vector3.Lerp(firstRotation, secondRotation, _blendValue);
            
            return true;
        }
        
        public void SetBlendValue(float value) => 
            _blendValue = Mathf.Clamp(value, 0, 1);
    }
}