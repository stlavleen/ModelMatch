


using System;
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

        public static float[,] ToArray(this Matrix4x4 m) 
        {
            return new float[,]
            {
                { m.m00, m.m01, m.m02, m.m03 },
                { m.m10, m.m11, m.m12, m.m13 },
                { m.m20, m.m21, m.m22, m.m23 },
                { m.m30, m.m31, m.m32, m.m33 }
            };
        }

        public static Matrix4x4 Round(this Matrix4x4 m) 
        {
            return new Matrix4x4 
            {
                m00 = Round(m.m00),
                m01 = Round(m.m01),
                m02 = Round(m.m02),
                m03 = Round(m.m03),
                m10 = Round(m.m10),
                m11 = Round(m.m11),
                m12 = Round(m.m12),
                m13 = Round(m.m13),
                m20 = Round(m.m20),
                m21 = Round(m.m21),
                m22 = Round(m.m22),
                m23 = Round(m.m23),
                m30 = Round(m.m30),
                m31 = Round(m.m31),
                m32 = Round(m.m32),
                m33 = Round(m.m33)
            };
        }

        private static float Round(float value) => (float)Math.Round(value, 10);
    }
}
