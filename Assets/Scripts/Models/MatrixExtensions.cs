using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelMatch.Models
{
    public static class MatrixExtensions
    {
        public static Matrix4x4 ToMatrix4x4(this Matrix<float> matrix) 
        {
            return new Matrix4x4
            {
                m00 = matrix.At(0, 0),
                m01 = matrix.At(0, 1),
                m02 = matrix.At(0, 2),
                m03 = matrix.At(0, 3),
                m10 = matrix.At(1, 0),
                m11 = matrix.At(1, 1),
                m12 = matrix.At(1, 2),
                m13 = matrix.At(1, 3),
                m20 = matrix.At(2, 0),
                m21 = matrix.At(2, 1),
                m22 = matrix.At(2, 2),
                m23 = matrix.At(2, 3),
                m30 = matrix.At(3, 0),
                m31 = matrix.At(3, 1),
                m32 = matrix.At(3, 2),
                m33 = matrix.At(3, 3)
            };
        }
    }
}
