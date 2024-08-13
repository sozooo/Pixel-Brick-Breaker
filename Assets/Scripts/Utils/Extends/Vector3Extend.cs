using UnityEngine;

public static class Vector3Extend
{
    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max)
    {
        if (value.x < min.x || value.y < min.y || value.z < min.z ||
            value.x > max.x || value.y > max.y || value.z > max.z)
        {
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);
            value.z = Mathf.Clamp(value.z, min.z, max.z);
        }

        return value;
    }

    public static Vector3 ClampY(this Vector3 value, float min, float max)
    {
        if (value.y < min || value.y > max)
        {
            value.y = Mathf.Clamp(value.y, min, max);
        }

        return value;
    }
}
