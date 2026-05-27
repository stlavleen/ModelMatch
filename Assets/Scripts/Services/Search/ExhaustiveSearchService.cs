
using ModelMatch.Models;
using System.Collections.Generic;
using System.Linq;
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
            var modelFirstPoint = modelPoints.FirstOrDefault();
            bool isMatched;
            Matrix4x4 currentOffset;
            HashSet<Matrix4x4> modelWithCurrentOffset;

            foreach (var spacePoint in spacePoints)
            {
                currentOffset = CalculateOffset(modelFirstPoint, spacePoint).Round();
                modelWithCurrentOffset = CreateModelWithOffset(modelPoints, currentOffset);
                isMatched = IsMatched(modelWithCurrentOffset, spacePoints);

                if (isMatched)
                    matches.Add(currentOffset);
            }

            return matches;
        }

        public Task<IEnumerable<Matrix4x4>> GetMatchesAsync(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints) 
        {
            return Task.Run(() => GetMatches(modelPoints, spacePoints));
        }
    }
}
