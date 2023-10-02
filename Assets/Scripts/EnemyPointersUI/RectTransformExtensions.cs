using System.Collections.Generic;
using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public static class RectTransformExtensions
    {
        public static IEnumerable<Vector2[]> GetSegments(this RectTransform rectTransform)
        {
            Vector3[] cornersArray = new Vector3[4];
            rectTransform.GetWorldCorners(cornersArray);

            for (int i = 0; i < cornersArray.Length; i++)
            {
                int secondCornerNumber = i + 1;

                if (secondCornerNumber == cornersArray.Length)
                    secondCornerNumber = 0;

                Vector2 firstCorner = cornersArray[i];
                Vector2 secondCorner = cornersArray[secondCornerNumber];

                yield return new[] {firstCorner, secondCorner};
            }
        }
    }
}