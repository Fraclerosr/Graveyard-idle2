using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class DebugOnBoundariesPointer : IPointerMovement
    {
        private readonly OnBoundariesPointer _onBoundariesPointer;

        public DebugOnBoundariesPointer(OnBoundariesPointer onBoundariesPointer)
        {
            _onBoundariesPointer = onBoundariesPointer;
        }

        public bool TryMove(Vector2 targetOnScreen, Vector2 fromPosition)
        {
            Debug.DrawLine(targetOnScreen, fromPosition, Color.red);
            Debug.Log(targetOnScreen);
            Debug.Log(fromPosition);
            
            return _onBoundariesPointer.TryMove(targetOnScreen, fromPosition);
        }
    }
}