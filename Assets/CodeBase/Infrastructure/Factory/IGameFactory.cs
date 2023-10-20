using System.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Task<GameObject> CreateSnake(Vector3 at);
  }
}