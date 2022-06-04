using UnityEngine;

namespace MyLibrary.Mathematic
{
    public class SimpleBezier : MonoBehaviour
    {
        public static Vector3 Bezier(Vector3 point1, Vector3 point2, Vector3 point3, double mu)
        {
            double mum1, mum12, mu2;
            Vector3 pos = Vector3.zero;

            mu2 = mu * mu;
            mum1 = 1 - mu;
            mum12 = mum1 * mum1;

            pos.x = (float) ((point1.x * mum12) +
                             (2 * point2.x * mum1 * mu) +
                             (point3.x * mu2));
            pos.y = (float) ((point1.y * mum12) +
                             (2 * point2.y * mum1 * mu) +
                             (point3.y * mu2));
            pos.z = (float) ((point1.z * mum12) +
                             (2 * point2.z * mum1 * mu) +
                             (point3.z * mu2));

            return pos;
        }

        public static Vector3 Bezier(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4,
            double mu)
        {
            double mum1, mum13, mu3;
            Vector3 pos = Vector3.zero;

            mum1 = 1 - mu;
            mum13 = mum1 * mum1 * mum1;
            mu3 = mu * mu * mu;

            pos.x = (float) ((mum13 * point1.x) +
                             (3 * mu * mum1 * mum1 * point2.x) +
                             (3 * mu * mu * mum1 * point3.x) +
                             (mu3 * point4.x));
            pos.y = (float) ((mum13 * point1.y) +
                             (3 * mu * mum1 * mum1 * point2.y) +
                             (3 * mu * mu * mum1 * point3.y) +
                             (mu3 * point4.y));
            pos.z = (float) ((mum13 * point1.z) +
                             (3 * mu * mum1 * mum1 * point2.z) +
                             (3 * mu * mu * mum1 * point3.z) +
                             (mu3 * point4.z));

            return pos;
        }
    }
}