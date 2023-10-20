using System.Threading.Tasks;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    // private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      // _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private async void OnLoaded()
    {
      await InitGameWorld();

      _stateMachine.Enter<GameState>();
    }

    private async Task InitGameWorld()
    {
      GameObject snake = await InitSnake();
      // CameraFollow(snake);
    }

    private async Task<GameObject> InitSnake()
    {
      await Task.Yield();
      return null;
      // return await _gameFactory.CreateSnake(GameObject.FindWithTag(InitialPointTag).transform.position);
    }
    
    // private void CameraFollow(GameObject snake) =>
    //   Camera.main.GetComponent<CameraFollow>().Follow(snake);
  }
}