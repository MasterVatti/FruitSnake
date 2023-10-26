using UnityEngine;

namespace CodeBase.Snake
{
  public class SnakeSizeIncrease : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private GameObject _snakeBodyPrefab;
    [SerializeField] private LayerMask _foodMask;
    [SerializeField] private SnakeBodyMovement _snakeBodyMovement;
    private float _snakeBodyCount = 1f;
    
    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
    }
    
    private void TriggerEnter(Collider obj)
    {
      if ((_foodMask.value & (1 << obj.gameObject.layer)) == 0)
      {
        GameObject body = Instantiate(_snakeBodyPrefab, transform);
        _snakeBodyMovement.AddSnakeBody(body);
      }
    }

    private void TriggerExit(Collider obj)
    {
    }
  }
}