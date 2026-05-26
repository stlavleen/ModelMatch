using ModelMatch.Services;
using UnityEngine;
using Zenject;

public class DataFileManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMatrix4x4FileManager>().To<Matrix4x4JsonFileManager>().AsCached();
    }
}