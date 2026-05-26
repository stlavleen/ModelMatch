
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelMatch.Services.Search
{
    // Exhaustive search. Slowest method. Approximate complexity = O(n * m) at least,
    // where n = modelPoints.Count, m = spacePoints.Count
    public class ExhaustiveSearchService : SearchService, ISearchService
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
    }
}
