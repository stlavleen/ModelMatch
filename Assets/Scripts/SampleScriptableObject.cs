
using UnityEngine;

[CreateAssetMenu(fileName = "Sample", menuName = "Scriptable Objects/Sample")]
public class SampleScriptableObject : ScriptableObject
{
    public GameObject original;
    public string path;
    public Color color;
}
