using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.States;
using Services.Input;
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
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      Container.Bind<SceneLoader>().AsSingle();
      Container.Bind<LoadingCurtain>().FromInstance(curtain).AsSingle();
      Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();


      Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromNewComponentOnNewGameObject().AsSingle();
    }

    // private void InputService()
    // {
    //   if (Application.isEditor)
    //     Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
    //   else
    //     Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
    // }
  }
}