using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Task<GameObject> CreateSnake(Vector3 at);
    Task<GameObject> CreateHud();
    GameObject SnakeGameObject { get; }
  }
}