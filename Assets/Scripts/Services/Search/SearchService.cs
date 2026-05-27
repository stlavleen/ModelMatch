
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using ModelMatch.Models;

namespace ModelMatch.Services.Search
{
    public class SearchService
    {
        protected Matrix4x4 CalculateOffset(Matrix4x4 modelPoint, Matrix4x4 spacePoint)
        {
            var modelMatrix = Matrix<float>.Build.DenseOfArray(modelPoint.ToArray());
            var spaceMatrix = Matrix<float>.Build.DenseOfArray(spacePoint.ToArray());
            var offset = modelMatrix.Solve(spaceMatrix);

            return offset.ToMatrix4x4();
        }

        protected Matrix4x4 GetPointWithOffset(Matrix4x4 point, Matrix4x4 offset)
        {
            return offset * point;
        }

        protected bool IsMatched(Matrix4x4 modelPoint, Matrix4x4 spacePoint, Matrix4x4 offset)
        {
            return offset * modelPoint == spacePoint;
        }

        protected HashSet<Matrix4x4> CreateModelWithOffset(HashSet<Matrix4x4> model, Matrix4x4 offset)
        {
            return model.Select(point => (point * offset).Round()).ToHashSet();
        }

        protected bool IsMatched(HashSet<Matrix4x4> model, HashSet<Matrix4x4> space)
        {
            return model.IsSubsetOf(space);
        }

        public Matrix4x4 Rotate(Matrix4x4 matrix, float x, float y, float z) 
        {
            var rotation = Quaternion.Euler(new Vector3(x, y, z));
            var rotated = Matrix4x4.TRS(matrix.GetPosition(), rotation, new Vector3(1, 1, 1));

            return rotated;
        }

        protected Matrix4x4 Translate(Matrix4x4 matrix, float x, float y, float z) 
        {
            var translated = Matrix4x4.TRS(new Vector3(x, y, z), matrix.rotation, new Vector3(1, 1, 1));

            return translated;
        }

        protected Matrix4x4 Scale(Matrix4x4 matrix, float x, float y, float z) 
        {
            var scaled = Matrix4x4.TRS(matrix.GetPosition(), matrix.rotation, new Vector3(x, y, z));

            return scaled;
        }
    }
}
