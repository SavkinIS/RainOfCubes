using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private LifecycleBehavior _lifecycleBehavior;
    [SerializeField] private ColorChanger _colorChanger;
    
    private CubesSpawner _cubesSpawner;
   
    private void OnEnable()
    {
        _collisionHandler.CollisionEnter += CollisionEnter;
        _lifecycleBehavior.CubeLifetimeEnded += LifetimeEnded;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionEnter -= CollisionEnter;
        _lifecycleBehavior.CubeLifetimeEnded -= LifetimeEnded;
    }
    
    public void SetSpawner(CubesSpawner cubesSpawner)
    {
        _cubesSpawner = cubesSpawner;
    }
    
    public void UpdateBeforeSpawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        _collisionHandler.ResetCollision();
        _colorChanger.SetBaseColor();
    }
    
    private void CollisionEnter()
    {
        _colorChanger.UpdateColor();
        _lifecycleBehavior.StartLifecycle();
    }
    
    private void LifetimeEnded()
    {
        _cubesSpawner.ReleasedToPool(this);
    }
}