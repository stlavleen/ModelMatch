
using ModelMatch.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelMatch.Services.Search
{
    public class ExhaustiveSearchService : SearchService, ISearchService
    {
        public IEnumerable<Matrix4x4> GetMatches(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints)
        {
            return GetMatchesImpl2(modelPoints, spacePoints);
        }

        private IEnumerable<Matrix4x4> GetMatchesImpl1(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints) 
        {
            var matches = new List<Matrix4x4>();
            var modelFirstPoint = modelPoints.FirstOrDefault();
            // var modelFirstPointPosition = modelFirstPoint.GetPosition();
            bool isMatched;
            Matrix4x4 currentOffset;
            HashSet<Matrix4x4> modelWithCurrentOffset;

            foreach (var spacePoint in spacePoints)
            {
                currentOffset = CalculateOffset(modelFirstPoint, spacePoint).Round();
                // var currentOffsetPosition = currentOffset.GetPosition();
                // var spacePointPosition = spacePoint.GetPosition();
                modelWithCurrentOffset = CreateModelWithOffset(modelPoints, currentOffset);

                //foreach (var mwo in modelWithCurrentOffset)
                //{
                //    var mwoPos = mwo.GetPosition();
                //}

                isMatched = IsMatched(modelWithCurrentOffset, spacePoints);

                if (isMatched)
                    matches.Add(currentOffset);
            }

            return matches;
        }

        private IEnumerable<Matrix4x4> GetMatchesImpl2(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints) 
        {
            var matches = new List<Matrix4x4>();
            var modelFirstPoint = modelPoints.FirstOrDefault();
            // var modelFirstPointPosition = modelFirstPoint.GetPosition();
            bool isMatched;
            Matrix4x4 currentOffset;
            HashSet<Matrix4x4> modelWithCurrentOffset;

            foreach (var spacePoint in spacePoints)
            {
                var currentOffsetVec3 = CalculateOffsetVec3(modelFirstPoint, spacePoint);
                currentOffset = Matrix4x4.Translate(currentOffsetVec3);
                // var currentOffsetPosition = currentOffset.GetPosition();
                // var spacePointPosition = spacePoint.GetPosition();
                modelWithCurrentOffset = CreateModelWithOffset(modelPoints, currentOffset);

                //foreach (var mwo in modelWithCurrentOffset)
                //{
                //    var mwoPos = mwo.GetPosition();
                //}

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
