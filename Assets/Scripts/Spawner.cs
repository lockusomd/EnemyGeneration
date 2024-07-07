using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy _prefab;
    private ObjectPool<GameObject> _pool;

    private int _defaultCapacity = 5;
    private int _maxSize = 10;
    private int _repeatRate = 2;

    private void OnEnable()
    {
        Enemy.Died += SendToPool;
    }

    private void OnDisable()
    {
        Enemy.Died -= SendToPool;
    }

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab.gameObject),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0.0f, _repeatRate);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = GetPosition();
        obj.GetComponent<Mover>().SetDirection(GetDirection().normalized);
        obj.SetActive(true);
    }

    private Vector3 GetPosition()
    {
        Vector3 position = new Vector3(GetSpawnPoint().x, GetSpawnPoint().y, GetSpawnPoint().z);

        return position;
    }

    private Vector3 GetSpawnPoint()
    {
        Vector3[] spawnPoints =
        {
            new Vector3(2,0,-2),
            new Vector3(2,0,2),
            new Vector3(-2,0,2),
            new Vector3(-2,0,-2)
        };

        int numberOfSpawn = Random.Range(0, spawnPoints.Length);

        return spawnPoints[numberOfSpawn];
    }

    private Vector3 GetDirection()
    {
        int numberX;
        int numberZ;

        do
        {
            numberX = Random.Range(-1, 1);
            numberZ = Random.Range(-1, 1);
        } while (numberX == 0 && numberZ == 0);

        Vector3 direction = new Vector3(numberX, 0, numberZ);

        return direction;
    }

    private Quaternion GetRotation()
    {
        Quaternion rotation = Quaternion.Euler(0,Random.Range(0,360),0);

        return rotation;
    }

    private void SendToPool(GameObject obj)
    {
        _pool.Release(obj);
    }
}
