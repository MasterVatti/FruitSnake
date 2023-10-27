using UnityEngine;

namespace CodeBase.Food
{
  public class FoodMoveToSnake : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private Collider _aggroCollider;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
      GetComponentInParent<SpawnFood>().EatFood(gameObject, _aggroCollider);
    }

    private void TriggerExit(Collider obj)
    {
    }
  }
}