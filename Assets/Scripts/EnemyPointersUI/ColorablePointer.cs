using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.EnemyPointersUI
{
    public class ColorablePointer : PointerBehaviour
    {
        [SerializeField] private Image _image;

        public void Point(Vector3 fromPosition, Vector3 targetPosition, Color color)
        {
            Point(fromPosition, targetPosition);
            _image.color = color;
        }
    }
}