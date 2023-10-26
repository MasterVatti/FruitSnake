using System.Collections;
using UnityEngine;

namespace CodeBase.Food
{
  public class AggroFood : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    // [SerializeField] private FoodMoveToSnake _foodMoveToSnake;
    [SerializeField] private float _movementSpeed;
    private bool _isEnter;
    private Vector3 _targetPosition;

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;

      // _foodMoveToSnake.enabled = false;
    }

    private void TriggerEnter(Collider obj)
    {
      // _targetPosition = obj.transform.position;
      // _targetPosition.Normalize();
      // _isEnter = true;
      gameObject.GetComponent<Collider>().enabled = false;
      StartCoroutine(Move(_targetPosition, obj));
      // transform.Translate(_targetPosition);
      Debug.Log($"{obj.transform.position} – TriggerEnter");
      // t.MovePosition(_snakeRigidbody.position +transform.TransformDirection(_inputService.Axis * _movementSpeed * Time.deltaTime));
    }

    private void TriggerExit(Collider obj)
    {
      StopAllCoroutines();
    }

    private void FixedUpdate()
    {
      if (_isEnter)
      {
        // transform.Translate(_targetPosition * _movementSpeed * Time.deltaTime);
      }
    }

    private IEnumerator Move(Vector3 targetPosition, Collider obj, float countTime = 0.02f)
    {
      float time = 1f;
      while (time > 0f)
      {
        Vector3 pos = obj.transform.position;
        time -= countTime;
        yield return new WaitForSeconds(countTime);
        // transform.Translate(targetPosition * _movementSpeed * Time.deltaTime);
        // transform.position = new Vector3(targetPosition.x,targetPosition.y,targetPosition.z).normalized;
        transform.position = Vector3.MoveTowards(transform.position, pos, _movementSpeed * Time.deltaTime);
      }
    }
  }
}