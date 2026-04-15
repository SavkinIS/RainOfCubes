using UnityEngine;

public class SpawnerCounterViewBase<T> : MonoBehaviour where T : ISpawnerInfo
{
    [SerializeField] protected TMPro.TextMeshProUGUI _totalSpawnedObjects;
    [SerializeField] protected TMPro.TextMeshProUGUI _totalCreatedObjects;
    [SerializeField] protected TMPro.TextMeshProUGUI _totalActiveObjects;
    
    [SerializeField] protected T _spawner;
    
    private void OnEnable()
    {
        _spawner.ObjectsCreated += UpdateTotalCreatedObjects;
        _spawner.ObjectSpawned += UpdateTotalSpawnedObjets;
        _spawner.ActiveObjectsCountChanged += UpdateActiveObjects;
    }

    private void OnDisable()
    {
        _spawner.ObjectsCreated -= UpdateTotalCreatedObjects;
        _spawner.ObjectSpawned -= UpdateTotalSpawnedObjets;
        _spawner.ActiveObjectsCountChanged -= UpdateActiveObjects;
    }
    
    
    private void UpdateActiveObjects(int count)
    {
        _totalActiveObjects.text = $"Всего активных объектов {count}";
    }

    private void UpdateTotalSpawnedObjets(int count)
    {
        _totalSpawnedObjects.text = $"Всего заспавнено объектов {count}";
    }

    private void UpdateTotalCreatedObjects(int count)
    {
        _totalCreatedObjects.text = $"Всего создано объектов {count}";
    }
}