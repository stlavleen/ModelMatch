

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModelMatch.Services
{
    public class Matrix4x4JsonFileManager : IMatrix4x4FileManager
    {
        readonly JsonSerializer serializer;

        public Matrix4x4JsonFileManager()
        {
            serializer = new JsonSerializer();
        }

        public IEnumerable<Matrix4x4> Read(string path)
        {
            IEnumerable<Matrix4x4> dataCollection = null;
            using (var reader = File.OpenText(path))
            {
                dataCollection = (IEnumerable<Matrix4x4>)serializer.Deserialize(reader, typeof(IEnumerable<Matrix4x4>));
            }

            return dataCollection;
        }

        public void Write(IEnumerable<Matrix4x4> dataCollection, string path)
        {
            using (var writer = File.CreateText(path))
            {
                serializer.Serialize(writer, dataCollection);
            }
        }
    }
}
