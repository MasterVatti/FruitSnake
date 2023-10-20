using System.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    private GameObject _snakeGameObject;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public async Task<GameObject> CreateSnake(Vector3 at) => _snakeGameObject = _assets.Instantiate(AssetPath.SnakePath, at);
  }
}