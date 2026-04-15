using System;
using UnityEngine;

public class BombesSpawner : SpawnerBase<Bomb>, ISpawnerInfo
{
    [SerializeField] private CubesSpawner _cubesSpawner;
    [SerializeField] private Bomb _bombPrefab;
    
    public event Action<int> ObjectsCreated;
    public event Action<int> ObjectSpawned;
    public event Action<int> ActiveObjectsCountChanged;

    public int TotalCreatedObjects => _pool.CountAll;
    public int ActiveOnSceneObjects => _pool.CountActive;
    public int TotalSpawnableObjects => _totalSpawned;
    
    private void OnEnable()
    {
        _cubesSpawner.CubeDestroyed += GetBomb;
    }

    private void OnDisable()
    {
        _cubesSpawner.CubeDestroyed -= GetBomb;
    }

    public override void ReleasedToPool(Bomb spawnableObject)
    {
        _pool.Release(spawnableObject);
        ActiveObjectsCountChanged?.Invoke(ActiveOnSceneObjects);
    }

    protected override void Release(Bomb spawnableObject)
    {
        spawnableObject.gameObject.SetActive(false);
    }

    protected override void OnGetNextSpawnableObject(Bomb spawnableObject)
    {
        spawnableObject.gameObject.SetActive(true);
        spawnableObject.Activate();
        _totalSpawned++;
        ObjectSpawned?.Invoke(TotalSpawnableObjects);
        ActiveObjectsCountChanged?.Invoke(ActiveOnSceneObjects);
    }

    protected override Bomb InstantiateSpawnableObject()
    {
        Bomb bomb = Instantiate(_bombPrefab, transform);
        bomb.SetSpawner(this);
        bomb.gameObject.SetActive(false);
        ObjectsCreated?.Invoke(TotalCreatedObjects);
        return bomb;
    }

    private void GetBomb(Cube cube)
    {
        _pool.Get(out Bomb bomb);
        bomb.UpdateBeforeSpawn(cube.transform.position);
    }
}