
using ModelMatch.Models;
using ModelMatch.Services;
using System.Collections.Generic;
using UnityEngine;

namespace ModelMatch 
{
    public class SamplesBehaviour : MonoBehaviour
    {
        IMatrix4x4JsonFileManager fileManager = new Matrix4x4JsonFileManager();

        public SampleScriptableObject modelSettings;
        public SampleScriptableObject spaceSettings;
        public string offsetPath;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InstantiateObjects(modelSettings.original, modelSettings.color, fileManager.Read(modelSettings.path));
            InstantiateObjects(spaceSettings.original, spaceSettings.color, fileManager.Read(spaceSettings.path));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InstantiateObjects(GameObject original, Color color, IEnumerable<Matrix4x4> objects) 
        {
            foreach (var data in objects)
            {
                var obj = GameObject.Instantiate(original);
                obj.transform.rotation = data.rotation;
                obj.transform.localScale = data.GetLocalScale();
                obj.transform.position = data.GetPosition();
                var renderer = obj.GetComponent<Renderer>();
                renderer.material.color = color;
            }
        }
    }
}


