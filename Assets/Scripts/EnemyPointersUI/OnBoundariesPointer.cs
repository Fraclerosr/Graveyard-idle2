using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public class OnBoundariesPointer : BoundariesPointer, IPointerMovement
    {
        public OnBoundariesPointer(RectTransform boundaries, Transform transform) : base(boundaries, transform)
        {
        }

        public bool TryMove(Vector2 targetOnScreen, Vector2 fromPosition)
        {
            bool inBoundaries = InBoundaries(fromPosition, targetOnScreen);

            if (inBoundaries)
                ShiftToBoundaries(fromPosition, targetOnScreen);

            return inBoundaries;
        }
    }
}