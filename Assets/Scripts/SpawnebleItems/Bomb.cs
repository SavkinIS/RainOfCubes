using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : SpawnableObject
{
    [SerializeField] private LifecycleBehavior _lifecycleBehavior;
    [SerializeField] private TransparencyChanger _transparencyChanger;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private LayerMask _layerMask;
    
    private BombesSpawner _bombesSpawner;

    private void OnEnable()
    {
        _lifecycleBehavior.LifetimeEnded += LifetimeEnded;
        _lifecycleBehavior.LifetimeChanged += _transparencyChanger.UpdateColor;
    }

    private void OnDisable()
    {
        _lifecycleBehavior.LifetimeEnded -= LifetimeEnded;
        _lifecycleBehavior.LifetimeChanged -= _transparencyChanger.UpdateColor;
    }
    
    public void SetSpawner(BombesSpawner bombesSpawner)
    {
        _bombesSpawner = bombesSpawner;
    }
    
    public void UpdateBeforeSpawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        _transparencyChanger.SetBaseColor();
    }
    
    public void Activate()
    {
        _lifecycleBehavior.StartLifecycle();
    }
    
    private void LifetimeEnded()
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _layerMask);
        
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        _bombesSpawner.ReleasedToPool(this);
    }
}