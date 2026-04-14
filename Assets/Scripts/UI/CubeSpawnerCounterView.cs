using UnityEngine;

public class CubeSpawnerCounterView : SpawnerCounterViewBase
{
    [SerializeField] private CubesSpawner _cubesSpawner;

    private void OnEnable()
    {
        _cubesSpawner.ObjectsCreated += UpdateTotalCreatedObjects;
        _cubesSpawner.ObjectSpawned += UpdateTotalSpawnedObjets;
        _cubesSpawner.ActiveObjectsCountChanged += UpdateActiveObjects;
    }

    private void OnDisable()
    {
        _cubesSpawner.ObjectsCreated -= UpdateTotalCreatedObjects;
        _cubesSpawner.ObjectSpawned -= UpdateTotalSpawnedObjets;
        _cubesSpawner.ActiveObjectsCountChanged -= UpdateActiveObjects;
    }
}