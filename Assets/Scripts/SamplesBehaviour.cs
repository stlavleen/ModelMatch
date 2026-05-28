
using ModelMatch.Models;
using ModelMatch.Services;
using ModelMatch.Services.Search;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ModelMatch 
{
    public class SamplesBehaviour : MonoBehaviour
    {
        [Inject] IMatrix4x4FileManager fileManager;
        [Inject] ISearchService searchService;

        public SampleScriptableObject modelSettings;
        public SampleScriptableObject spaceSettings;
        public string offsetPath;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        async void Start()
        {
            var modelPoints = fileManager.Read(modelSettings.path);
            var spacePoints = fileManager.Read(spaceSettings.path);
            InstantiateObjects(modelSettings.original, modelSettings.color, modelPoints);
            InstantiateObjects(spaceSettings.original, spaceSettings.color, spacePoints);
            UnityEngine.Debug.LogWarning("Search has been started. Please wait...");
            var matches = await searchService.GetMatchesAsync(modelPoints.ToHashSet(), spacePoints.ToHashSet());
            UnityEngine.Debug.LogWarning($"Search is completed. Count = {matches.Count()}");
            if (matches.Count() != 0 && !string.IsNullOrEmpty(offsetPath))
                fileManager.Write(matches.Select(x => x.ToPOD()), offsetPath);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InstantiateObjects(GameObject original, Color color, IEnumerable<Matrix4x4> points) 
        {
            foreach (var point in points)
            {
                var obj = GameObject.Instantiate(original);
                obj.transform.rotation = point.rotation;
                obj.transform.localScale = point.GetLocalScale();
                obj.transform.position = point.GetPosition();
                var renderer = obj.GetComponent<Renderer>();
                renderer.material.color = color;
            }
        }

        private void RotatePointsAndWriteToFile(IEnumerable<Matrix4x4> model, IEnumerable<Matrix4x4> space, string dir) 
        {
            Vector3 rotation = new Vector3(45, 0, 0);
            RotatePointsAndWriteToFile(model.ToArray(), rotation, dir, "model.json");
            RotatePointsAndWriteToFile(space.ToArray(), -rotation, dir, "space.json");
        }

        private void RotatePointsAndWriteToFile(Matrix4x4[] obj, Vector3 rotation, string dir, string fileName)
        {
            for (int i = 0; i < obj.Length; i++) 
            {
                var k = i % 2 == 0 ? 0 : 1;
                obj[i] = searchService.Rotate(obj[i], rotation * k);
            }  

            fileManager.Write(obj.Select(x => x.ToPOD()), Path.Combine(dir, fileName));
        }
    }
}


