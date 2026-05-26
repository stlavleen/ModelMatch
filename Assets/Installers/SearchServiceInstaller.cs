
using ModelMatch.Services;
using Zenject;

public class SearchServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISearchService>().To<ExhaustiveSearchService>().AsSingle().NonLazy();
    }
}