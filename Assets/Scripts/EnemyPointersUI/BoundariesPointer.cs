using System;
using System.Linq;
using Source.Common.Math;
using UnityEngine;

namespace Source.UI.EnemyPointersUI
{
    public abstract class BoundariesPointer
    {
        private readonly RectTransform _boundaries;
        private readonly Transform _transform;

        protected BoundariesPointer(RectTransform boundaries, Transform transform)
        {
            _boundaries = boundaries;
            _transform = transform;
        }

        protected bool InBoundaries(Vector2 fromPosition, Vector3 targetOnScreen)
        {
            return _boundaries.GetSegments().Any(segment =>
                VectorsIntersection.AreSegmentsIntersecting(segment[0], segment[1], fromPosition, targetOnScreen));
        }

        protected void ShiftToBoundaries(Vector2 fromPosition, Vector3 targetOnScreen)
        {
            Vector2? intersectionPoint = null;

            foreach (Vector2[] segment in _boundaries.GetSegments())
            {
                if (!VectorsIntersection.AreSegmentsIntersecting(segment[0], segment[1], fromPosition, targetOnScreen))
                    continue;

                intersectionPoint =
                    VectorsIntersection.GetLinesIntersectionPoint(segment[0], segment[1], fromPosition, targetOnScreen);
            }

            if (intersectionPoint == null)
                throw new InvalidOperationException();

            _transform.position = intersectionPoint.Value;
        }
    }
}