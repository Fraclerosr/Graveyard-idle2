using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public interface IPointerRotation
    {
        void RotateToDirection(Vector2 screenDirection);
    }
}