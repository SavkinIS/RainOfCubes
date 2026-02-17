using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeSpawnPlate;
    [SerializeField] private float _spawnDelay;

    private int _poolCapacity = 50;
    private RandomPosition _randomPosition;
    private ObjectPool<Cube> _pool;
    private float _elapsedTime = 0f;
    private bool _isTicking = true;
    private Coroutine _spawnCubesCoroutine;

    public event Action<Cube> CubeSpawned;

    private void Start()
    {
        _randomPosition = new RandomPosition(_cubeSpawnPlate);

        _pool = new ObjectPool<Cube>(
            createFunc: InstatiateCube,
            actionOnGet: Getted,
            actionOnRelease: Released,
            defaultCapacity: _poolCapacity,
            maxSize: 100);
    }

    private void OnEnable()
    {
        _spawnCubesCoroutine = StartCoroutine(SpawnCubesCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCubesCoroutine);
    }

    private void OnDestroy()
    {
        CubeSpawned =  null;
    }

    public void ReleasedToPool(Cube cube)
    {
        _pool.Release(cube);
    }

    private void Released(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void Getted(Cube cube)
    {
        cube.UpdateBeforeSpawn(_randomPosition.GetRandomPosition());
        cube.gameObject.SetActive(true);
        CubeSpawned?.Invoke(cube);
    }

    private Cube InstatiateCube()
    {
        Cube cube = Instantiate(_cubePrefab, transform);
        cube.gameObject.SetActive(false);
        return cube;
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        while (_isTicking)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _spawnDelay)
            {
                _elapsedTime = 0;
                _pool.Get(out Cube cube);
            }

            yield return null;
        }
    }
}