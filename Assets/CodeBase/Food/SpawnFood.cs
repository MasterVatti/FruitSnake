using System.Collections;
using CodeBase.Infrastructure.ObjectPools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Food
{
  public class SpawnFood : MonoBehaviour
  {
    [SerializeField] private Food _foodPrefab;
    [SerializeField] private int _foodCount;
    
    private GameObject _ground;
    private MonoBehaviourPool<Food> _foodPool;
    private Vector3[] _vertices;

    private void Awake()
    {
      _ground = GameObject.FindWithTag("Planet");
      _foodPool = new MonoBehaviourPool<Food>(_foodPrefab, transform, _foodCount);
    }

    private void Start()
    {
      SpawnAllFood();
    }
    
    public void SpawnOneFood()
    {
      Food food = _foodPool.Take();
      SetFoodPosition(food, _vertices);
    }

    public void EatFood(GameObject food)
    {
      Food foodInPool = food.GetComponent<Food>();
      _foodPool.Release(foodInPool);
      StartCoroutine(FoodCooldown());
      StopCoroutine(FoodCooldown());
      
      // Debug.Log("EndFoodCooldown");
    }

    private IEnumerator FoodCooldown(float countTime = 2.5f)
    {
      // Debug.Log("StartFoodCooldown");
      yield return new WaitForSeconds(countTime);
      SpawnOneFood();
    }

    private void SpawnAllFood()
    {
      Mesh sphereMesh = _ground.GetComponent<MeshFilter>().sharedMesh;
      _vertices = sphereMesh.vertices;
      for (int i = 0; i < _foodCount; i++) SpawnOneFood();
    }

    private void SetFoodPosition(Food food, Vector3[] vertices)
    {
      int randomIndex = Random.Range(0, vertices.Length);
      Vector3 position = _ground.transform.TransformPoint(vertices[randomIndex]);
      food.transform.position = position;
      food.transform.rotation = Quaternion.identity;
    }
  }
}