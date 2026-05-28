


using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModelMatch.Services.Search
{
    public class ExhaustiveSearchServiceB : SearchService, ISearchService
    {
        public override IEnumerable<Matrix4x4> GetMatches(IEnumerable<Matrix4x4> modelPoints, IEnumerable<Matrix4x4> spacePoints)
        {
            return GetMatchesImpl(modelPoints.ToArray(), spacePoints.ToArray());
        }

        private IEnumerable<Matrix4x4> GetMatchesImpl(Matrix4x4[] modelPoints, Matrix4x4[] spacePoints) 
        {
            var matches = new List<Matrix4x4>();
            bool offsetCalculated;
            bool isMatched;
            Vector3 currentOffsetVec3 = default;
            Matrix4x4 currentOffset = default;
            IEnumerable<Matrix4x4> modelWithCurrentOffset;

            foreach (var modelPoint in modelPoints)
            {
                offsetCalculated = false;
                isMatched = false;

                foreach (var spacePoint in spacePoints)
                {
                    if (!offsetCalculated)
                    {
                        currentOffsetVec3 = CalculateOffsetVec3(modelPoint, spacePoint);
                        currentOffset = Matrix4x4.Translate(currentOffsetVec3);
                        offsetCalculated = true;
                    }

                    modelWithCurrentOffset = CreateModelWithOffset(modelPoints, currentOffsetVec3).ToArray();
                    isMatched = IsMatched(modelWithCurrentOffset, spacePoints);
                }

                if (isMatched)
                    matches.Add(currentOffset);
            }


            return matches;
        }
    }
}
