using UnityEngine;

public class BombSpawnerCounterView : SpawnerCounterViewBase
{
    [SerializeField] private BombesSpawner _bombSpawner;

    private void OnEnable()
    {
        _bombSpawner.ObjectsCreated += UpdateTotalCreatedObjects;
        _bombSpawner.ObjectSpawned += UpdateTotalSpawnedObjets;
        _bombSpawner.ActiveObjectsCountChanged += UpdateActiveObjects;
    }

    private void OnDisable()
    {
        _bombSpawner.ObjectsCreated -= UpdateTotalCreatedObjects;
        _bombSpawner.ObjectSpawned -= UpdateTotalSpawnedObjets;
        _bombSpawner.ActiveObjectsCountChanged -= UpdateActiveObjects;
    }
}