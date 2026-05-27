

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelMatch.Services.Search
{
    public interface ISearchService
    {
        IEnumerable<Matrix4x4> GetMatches(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints);
        Task<IEnumerable<Matrix4x4>> GetMatchesAsync(HashSet<Matrix4x4> modelPoints, HashSet<Matrix4x4> spacePoints);
        Matrix4x4 Rotate(Matrix4x4 matrix, float x, float y, float z);
    }
}
