using System;

public interface ISpawnerInfo
{
    event Action<int> ObjectsCreated;
    event Action<int> ObjectSpawned;
    event Action<int> ActiveObjectsCountChanged;
    
    int TotalSpawnableObjects { get; }
    int TotalCreatedObjects { get; }
    int ActiveOnSceneObjects { get; }
}