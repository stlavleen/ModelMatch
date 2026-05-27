
using ModelMatch.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ModelMatch.Services
{
    public interface IMatrix4x4FileManager
    {
        IEnumerable<Matrix4x4> Read(string path);
        void Write(IEnumerable<Matrix4x4POD> dataCollection, string path);
    }
}
