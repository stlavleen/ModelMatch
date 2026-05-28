
using ModelMatch.Services.Search;
using Zenject;

public class SearchServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISearchService>().To<ExhaustiveSearchServiceB>().AsSingle().NonLazy();
    }
}