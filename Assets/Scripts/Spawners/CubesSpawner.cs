using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : SpawnerBase<Cube>, ISpawnerInfo
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeSpawnPlate;
    [SerializeField] private float _spawnDelay;

    private PositionRandomizer _positionRandomizer;
    private float _elapsedTime = 0f;
    private bool _isTicking = true;
    private Coroutine _spawnCubesCoroutine;

    public event Action<Cube> CubeDestroyed;
    
    public event Action<int> ObjectsCreated;
    public event Action<int> ObjectSpawned;
    public event Action<int> ActiveObjectsCountChanged;

    public int TotalCreatedObjects => _pool.CountAll;
    public int ActiveOnSceneObjects => _pool.CountActive;
    public int TotalSpawnableObjects => _totalSpawned;
    
    private void Awake()
    {
        _positionRandomizer = new PositionRandomizer(_cubeSpawnPlate);
    }
    
    private void OnEnable()
    {
        _spawnCubesCoroutine = StartCoroutine(SpawnCubesCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCubesCoroutine);
    }

    public override void ReleasedToPool(Cube spawnableObject)
    {
        _pool.Release(spawnableObject);
        CubeDestroyed?.Invoke(spawnableObject);
        ActiveObjectsCountChanged?.Invoke(ActiveOnSceneObjects);
    }

    protected override void Release(Cube spawnableObject)
    {
        spawnableObject.gameObject.SetActive(false);
    }

    protected override void OnGetNextSpawnableObject(Cube spawnableObject)
    {
        spawnableObject.UpdateBeforeSpawn(_positionRandomizer.GetRandomPosition());
        spawnableObject.gameObject.SetActive(true);
    }

    protected override Cube InstantiateSpawnableObject()
    {
        Cube cube = Instantiate(_cubePrefab, transform);
        cube.SetSpawner(this);
        cube.gameObject.SetActive(false);
        ObjectsCreated?.Invoke(TotalCreatedObjects);
        return cube;
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        
        while (_isTicking)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _spawnDelay)
            {
                _elapsedTime = 0;
                _pool.Get(out Cube cube);
                _totalSpawned++;
                ObjectSpawned?.Invoke(TotalSpawnableObjects);
                ActiveObjectsCountChanged?.Invoke(ActiveOnSceneObjects);
            }

            yield return null;
        }
    }
 
}