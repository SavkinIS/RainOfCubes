using System;
using UnityEngine;

public class SpawnerCounterViewBase : MonoBehaviour
{
    [SerializeField] protected TMPro.TextMeshProUGUI _totalSpawnedObjects;
    [SerializeField] protected TMPro.TextMeshProUGUI _totalCreatedObjects;
    [SerializeField] protected TMPro.TextMeshProUGUI _totalActiveObjects;
    
    protected void UpdateActiveObjects(int count)
    {
        _totalActiveObjects.text = $"Всего активных объектов {count}";
    }

    protected void UpdateTotalSpawnedObjets(int count)
    {
        _totalSpawnedObjects.text = $"Всего заспавнено объектов {count}";
    }

    protected void UpdateTotalCreatedObjects(int count)
    {
        _totalCreatedObjects.text = $"Всего создано объектов {count}";
    }
}