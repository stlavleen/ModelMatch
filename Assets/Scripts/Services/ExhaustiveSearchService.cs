


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelMatch.Services
{
    // Exhaustive search. Slowest method. Approximate complexity = O(n * m) at least,
    // where n = modelPoints.Count, m = spacePoints.Count
    public class ExhaustiveSearchService : ISearchService
    {
        public IEnumerable<Matrix4x4> GetMatches(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints)
        {
            var matches = new List<Matrix4x4>();

            foreach (var modelPoint in modelPoints) 
            {
                foreach (var spacePoint in spacePoints) 
                {
                    var offset = CalculateOffset(modelPoint, spacePoint);
                    var modelWithOffset = CreateModelWithOffset(modelPoints, offset);
                    var isMatched = IsMatched(modelWithOffset, spacePoints);

                    if (isMatched)
                        matches.Add(offset);
                }
            }

            return matches;
        }

        public Task<IEnumerable<Matrix4x4>> GetMatchesAsync(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints) 
        {
            return Task.Run(() => GetMatches(modelPoints, spacePoints));
        }

        private Matrix4x4 CalculateOffset(Matrix4x4 modelPoint, Matrix4x4 spacePoint)
        {
            return new Matrix4x4(); // TODO
        }

        private Matrix4x4 GetPointWithOffset(Matrix4x4 point, Matrix4x4 offset)
        {
            return offset * point;
        }

        private bool IsMatched(Matrix4x4 modelPoint, Matrix4x4 spacePoint, Matrix4x4 offset)
        {
            return offset * modelPoint == spacePoint;
        }

        private HashSet<Matrix4x4> CreateModelWithOffset(HashSet<Matrix4x4> model, Matrix4x4 offset)
        {
            return model.Select(point => point * offset).ToHashSet();
        }

        private bool IsMatched(HashSet<Matrix4x4> model, HashSet<Matrix4x4> space)
        {
            return model.IsSubsetOf(space);
        }
    }
}
