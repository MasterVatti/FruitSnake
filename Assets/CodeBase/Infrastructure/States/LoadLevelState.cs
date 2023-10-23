using System.Threading.Tasks;
using CameraLogic;
using Infrastructure.Factory;
using Services.Input;
using Snake;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IInputService _inputService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IInputService inputService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _inputService = inputService;
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
      snake.GetComponent<SnakeMove>().Constructor(_inputService);
      CameraFollow(snake);
    }

    private async Task<GameObject> InitSnake()
    {
      return await _gameFactory.CreateSnake(GameObject.FindWithTag(InitialPointTag).transform.position);
    }
    
    private void CameraFollow(GameObject snake) =>
      Camera.main.GetComponent<CameraFollow>().Follow(snake);
  }
}