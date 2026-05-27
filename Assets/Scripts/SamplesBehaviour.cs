
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
            var matches = await searchService.GetMatchesAsync(modelPoints.ToHashSet(), spacePoints.ToHashSet());
            UnityEngine.Debug.LogWarning($"Search is completed. Count = {matches.Count()}");
            if (matches.Count() != 0 && !string.IsNullOrEmpty(offsetPath))
                fileManager.Write(matches, offsetPath);
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
    }
}


