using UnityEngine;

namespace MyLibrary.Mathematic
{
    // 2차 : 단순 직선
    public class LinearBezierCurve : BezierCurveBase
    {
        [Space]
        public Transform pointA;
        public Transform pointB;

        private Vector3 lerpAB;

        private void OnValidate()
        {
            if (pointA == null || pointB == null) return;

            lerpAB = Lerp(pointA, pointB, progression);
        }

        private void OnDrawGizmos()
        {
            if (pointA == null || pointB == null) return;

            float radius = gizmoRadius;

            Gizmos.color = Color.red;
            DrawGizmoSpheres(radius, pointA, pointB);
            //DrawGizmoLine(pointA, pointB);

            Gizmos.color = Color.yellow;
            radius *= 0.8f;
            DrawGizmoSphere(radius, lerpAB);
            DrawGizmoLine(pointA.position, lerpAB);
        }
    }
}