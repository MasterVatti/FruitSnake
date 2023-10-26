using UnityEngine;

namespace CodeBase.Food
{
  public class FoodMoveToSnake : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;

    // private IGameFactory _gameFactory;
    //
    // [Inject]
    // public void Constructor(IGameFactory gameFactory)
    // {
    //   Debug.Log($"Constructor {gameFactory}");
    //   _gameFactory = gameFactory;
    // }

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
      Debug.Log("FoodMoveToSnake TriggerEnter");
      GetComponentInParent<SpawnFood>().EatFood(gameObject);
    }

    private void TriggerExit(Collider obj)
    {
    }
  }
}