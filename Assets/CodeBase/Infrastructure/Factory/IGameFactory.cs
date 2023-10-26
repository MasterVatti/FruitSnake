using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Task<GameObject> CreateSnake(Vector3 at);
    GameObject SnakeGameObject { get; }
  }
}