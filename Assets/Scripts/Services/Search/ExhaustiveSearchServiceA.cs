
using ModelMatch.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModelMatch.Services.Search
{
    public class ExhaustiveSearchServiceA : SearchService, ISearchService
    {
        public override IEnumerable<Matrix4x4> GetMatches(IEnumerable<Matrix4x4> modelPoints, IEnumerable<Matrix4x4> spacePoints)
        {
            var matches = new List<Matrix4x4>();
            var modelFirstPoint = modelPoints.FirstOrDefault();
            bool isMatched;
            Matrix4x4 currentOffset;
            IEnumerable<Matrix4x4> modelWithCurrentOffset;

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

        
    }
}
