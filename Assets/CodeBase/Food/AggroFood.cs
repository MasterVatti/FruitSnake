using System.Collections;
using UnityEngine;

namespace CodeBase.Food
{
  public class AggroFood : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private float _movementSpeed;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider target)
    {
      gameObject.GetComponent<Collider>().enabled = false;
      StartCoroutine(MoveFood(target));
    }

    private void TriggerExit(Collider obj)
    {
      StopAllCoroutines();
    }

    private IEnumerator MoveFood(Collider target, float countTime = 0.02f)
    {
      float time = 1.6f;
      while (time > 0f)
      {
        Vector3 targetPosition = target.transform.position;
        time -= countTime;
        yield return new WaitForSeconds(countTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);
      }
    }
  }
}