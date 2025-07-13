using UnityEngine;

namespace Project.Scripts.Utils.Extends
{
    public static class Vector3Extend
    {
        public static Vector3 ClampX(this Vector3 value, float min, float max)
        {
            if (value.x < min || value.x > max)
            {
                value.x = Mathf.Clamp(value.x, min, max);
            }

            return value;
        }
    }
}
