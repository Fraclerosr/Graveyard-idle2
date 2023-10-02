using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public interface IPointerMovement
    {
        bool TryMove(Vector2 targetOnScreen, Vector2 fromPosition);
    }
}