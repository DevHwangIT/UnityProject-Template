using UnityEngine;

namespace MyLibrary.Mathematic
{
    public class MathUtility
    {
        public static float JumpCalc(float time, float power, float gravity, float scale = 1.0f)
        {
            var addValue = (time * time * (-gravity * scale) / 2)
                           + (time * power * scale);
            return addValue;
        }

        public static float GetPercent(float wholeValue, float maximumValue)
        {
            var value = wholeValue / maximumValue;
            value = Mathf.Clamp(value, 0, 1) * 100f;
            return value;
        }

        public static float GetAngle(Vector3 start, Vector3 end)
        {
            Vector3 diff = end - start;
            return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        }

        public static Vector3 GetRoundPos(Vector3 center, float min, float max)
        {
            float dis = UnityEngine.Random.Range(min, max);
            float angle = UnityEngine.Random.Range(0.0f, 360.0f);
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * dis;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * dis;

            var pos = new Vector3(x, 0.0f, y) + center;

            return pos;
        }

        public static Vector3 GetSideVector3(float value, Vector3 dir)
        {
            Quaternion rotate = Quaternion.AngleAxis(value, Vector3.up);
            var sideDir = rotate * dir;
            return sideDir;
        }

        public static Vector3 GetFrontVector3(float value, Vector3 dir)
        {
            Quaternion rotate = Quaternion.AngleAxis(value, Vector3.right);
            var sideDir = rotate * dir;
            return sideDir;
        }
    }
}