using Infrastructure.ObjectPools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Food
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
      _ground = GameObject.FindWithTag("Ground");
      _foodPool = new MonoBehaviourPool<Food>(_foodPrefab, transform, _foodCount);
    }

    private void Start()
    {
      SpawnAllFood();
    }
    
    public void SpawnOneFood()
    {
      Food food = _foodPool.Take();
      SetFoodPosition(_vertices);
    }

    public void EatFood(Food food)
    {
      _foodPool.Release(food);
    }
    
    private void SpawnAllFood()
    {
      Mesh sphereMesh = _ground.GetComponent<MeshFilter>().sharedMesh;
      _vertices = sphereMesh.vertices;
      for (int i = 0; i < _foodCount; i++) SpawnOneFood();
    }

    private void SetFoodPosition(Vector3[] vertices)
    {
      int randomIndex = Random.Range(0, vertices.Length);
      Vector3 position = _ground.transform.TransformPoint(vertices[randomIndex]);

      Food newObject = Instantiate(_foodPrefab, position, Quaternion.identity);
      newObject.transform.SetParent(transform);
    }
  }
}