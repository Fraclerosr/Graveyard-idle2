using System;
using UnityEngine;

namespace Source.Common.Math
{
    public static class VectorsIntersection
    {
        public static bool AreSegmentsIntersecting(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
        {
            float denominator = (B2.y - B1.y) * (A2.x - A1.x) - (B2.x - B1.x) * (A2.y - A1.y);

            if (denominator == 0)
                return false;

            float u_a = ((B2.x - B1.x) * (A1.y - B1.y) - (B2.y - B1.y) * (A1.x - B1.x)) / denominator;
            float u_b = ((A2.x - A1.x) * (A1.y - B1.y) - (A2.y - A1.y) * (A1.x - B1.x)) / denominator;

            return u_a >= 0 && u_a <= 1 && u_b >= 0 && u_b <= 1;
        }

        public static Vector2 GetLinesIntersectionPoint(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
        {
            float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

            if (tmp == 0)
                throw new InvalidOperationException();

            float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;

            return new Vector2(
                B1.x + (B2.x - B1.x) * mu,
                B1.y + (B2.y - B1.y) * mu
            );
        }
    }
}