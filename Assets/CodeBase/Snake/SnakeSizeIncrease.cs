using UnityEngine;

namespace CodeBase.Snake
{
  public class SnakeSizeIncrease : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private GameObject _snakeBodyPrefab;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private LayerMask _foodMask;
    [SerializeField] private SnakeBodyMovement _snakeBodyMovement;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
      if ((_foodMask.value & (1 << obj.gameObject.layer)) == 0)
      {
        GameObject body = Instantiate(_snakeBodyPrefab, _parentTransform);
        _snakeBodyMovement.AddSnakeBodyPart(body);
      }
    }

    private void TriggerExit(Collider obj)
    {
    }
  }
}