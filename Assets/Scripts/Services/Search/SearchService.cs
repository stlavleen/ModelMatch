
using ModelMatch.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelMatch.Services.Search
{
    public abstract class SearchService
    {
        protected Matrix4x4 CalculateOffset(Matrix4x4 modelPoint, Matrix4x4 spacePoint) => 
            Matrix4x4.Translate(CalculateOffsetVec3(modelPoint, spacePoint));

        protected Vector3 CalculateOffsetVec3(Matrix4x4 modelPoint, Matrix4x4 spacePoint) => 
            spacePoint.GetPosition() - modelPoint.GetPosition();

        protected Vector4 CalculateOffsetVec4(Matrix4x4 modelPoint, Matrix4x4 spacePoint) 
        {
            var diff = spacePoint.GetPosition() - modelPoint.GetPosition();

            return new Vector4(diff.x, diff.y, diff.z, 1);
        }

        protected Matrix4x4 GetPointWithOffset(Matrix4x4 point, Matrix4x4 offset)
        {
            return offset * point;
        }

        protected bool IsMatched(Matrix4x4 modelPoint, Matrix4x4 spacePoint, Matrix4x4 offset)
        {
            return offset * modelPoint == spacePoint;
        }

        protected bool IsMatched(Vector3 modelPointPosition, Vector3 spacePointPosition, Vector3 offset) 
        {
            return modelPointPosition + offset == spacePointPosition;
        }

        protected bool IsMatched(IEnumerable<Matrix4x4> model, IEnumerable<Matrix4x4> space)
        {
            return model.ToHashSet().IsSubsetOf(space);
        }

        protected IEnumerable<Matrix4x4> CreateModelWithOffset(IEnumerable<Matrix4x4> model, Matrix4x4 offset)
        {
            return model.Select(point =>
            {
                Matrix4x4 newPoint = point;
                Vector4 pointPositionVector = point.GetColumn(3);
                Vector4 displacedPositionVector = offset * pointPositionVector;
                newPoint.SetColumn(3, displacedPositionVector);
                return newPoint;
            });
        }

        protected IEnumerable<Matrix4x4> TranslateModelToPosition(IEnumerable<Matrix4x4> model, Vector3 position)
        {
            return model.Select(point => Translate(point, position.x, position.y, position.z).Round()).ToHashSet();
        }

        public Matrix4x4 Rotate(Matrix4x4 matrix, Vector3 rotation) 
        {
            var rotationQ = Quaternion.Euler(rotation);
            var rotated = Matrix4x4.TRS(matrix.GetPosition(), rotationQ, new Vector3(1, 1, 1));

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

        public Task<IEnumerable<Matrix4x4>> GetMatchesAsync(IEnumerable<Matrix4x4> modelPoints, IEnumerable<Matrix4x4> spacePoints)
        {
            return Task.Run(() => GetMatches(modelPoints, spacePoints));
        }

        public abstract IEnumerable<Matrix4x4> GetMatches(IEnumerable<Matrix4x4> modelPoints, IEnumerable<Matrix4x4> spacePoints);
    }
}
