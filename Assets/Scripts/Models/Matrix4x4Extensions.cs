


using UnityEngine;

namespace ModelMatch.Models
{
    public static class Matrix4x4Extensions
    {
        /// <summary>
        /// See https://discussions.unity.com/t/how-to-assign-matrix4x4-to-transform/467216/2
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Vector3 GetLocalScale(this Matrix4x4 m) 
        {
            Vector3 scale;
            scale.x = new Vector4(m.m00, m.m10, m.m20, m.m30).magnitude;
            scale.y = new Vector4(m.m01, m.m11, m.m21, m.m31).magnitude;
            scale.z = new Vector4(m.m02, m.m12, m.m22, m.m32).magnitude;
            return scale;
        }
    }
}
