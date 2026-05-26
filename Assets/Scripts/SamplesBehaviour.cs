
using ModelMatch.Models;
using ModelMatch.Services;
using ModelMatch.Services.Search;
using System.Collections.Generic;
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
            TranslateFirstPointToSpace(modelPoints.ToHashSet(), spacePoints.FirstOrDefault());
            var matches = await searchService.GetMatchesAsync(RotateFirstPointTo90degrees(modelPoints.ToHashSet()), spacePoints.ToHashSet());
            UnityEngine.Debug.LogWarning($"Search is completed. Count = {matches.Count()}");
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

        private HashSet<Matrix4x4> RotateFirstPointTo90degrees(HashSet<Matrix4x4> points) 
        {
            var rotation0_0_90 = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            var rotation0_0_0 = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            var rotation0_0_180 = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            var testRotationMatrix0_0_90 = Matrix4x4.Rotate(rotation0_0_90);
            var testRotationMatrix0_0_0 = Matrix4x4.Rotate(rotation0_0_0);
            var testRotationMatrix0_0_180 = Matrix4x4.Rotate(rotation0_0_180);
            var first = points.FirstOrDefault();
            var newModelPoint = testRotationMatrix0_0_90 * first;
            points.RemoveWhere(x => x.Equals(first));
            points.Add(newModelPoint);

            return points;
        }

        private void TranslateFirstPointToSpace(HashSet<Matrix4x4> modelPoints, Matrix4x4 spacePoints) 
        {
            var offset = Matrix4x4.Translate(spacePoints.GetPosition());
        }
    }
}


