using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Input;
using CodeBase.Snake;
using UnityEngine;

namespace CodeBase.Infrastructure.States
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
      GameObject hud = await InitHud();
      snake.GetComponentInChildren<SnakeMovement>().Constructor(_inputService);
      CameraFollow(snake);
    }

    private async Task<GameObject> InitSnake() =>
      await _gameFactory.CreateSnake(GameObject.FindWithTag(InitialPointTag).transform.position);

    private async Task<GameObject> InitHud() => await _gameFactory.CreateHud();

    private void CameraFollow(GameObject snake) =>
      Camera.main.GetComponent<CameraFollow>().Follow(snake.GetComponentInChildren<SnakeBodyMovement>().gameObject);
  }
}