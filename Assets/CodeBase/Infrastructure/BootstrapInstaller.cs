using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private GameObject _curtainPrefab;

    public override void InstallBindings()
    {
      LoadingCurtain curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(_curtainPrefab);
      Container.Bind<LoadingCurtain>().FromInstance(curtain).AsSingle();
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      Container.Bind<SceneLoader>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
      
      Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromNewComponentOnNewGameObject().AsSingle();
    }
    
  }
}