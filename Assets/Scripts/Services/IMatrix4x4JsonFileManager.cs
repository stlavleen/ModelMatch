
using System.Collections.Generic;
using UnityEngine;

namespace ModelMatch.Services
{
    internal interface IMatrix4x4JsonFileManager
    {
        IEnumerable<Matrix4x4> Read(string path);
        void Write(IEnumerable<Matrix4x4> dataCollection, string path);
    }
}
