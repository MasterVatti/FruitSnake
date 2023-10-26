using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Services.Input
{
  public interface IInputService : IService
  {
    Vector3 Axis { get; }

    bool IsAttackButtonUp();
  }
}