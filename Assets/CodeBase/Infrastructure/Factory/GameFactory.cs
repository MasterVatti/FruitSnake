using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public GameObject SnakeGameObject { get; private set; }
    private readonly IAssetProvider _assets;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public async Task<GameObject> CreateSnake(Vector3 at) => SnakeGameObject = _assets.Instantiate(AssetPath.SnakePath, at);
    public async Task<GameObject> CreateHud() => _assets.Instantiate(AssetPath.HudPath);
  }
}