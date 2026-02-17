using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Cube> CollisionEnter;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out Cube cube))
        {
            CollisionEnter?.Invoke(cube);
        }
    }

    private void OnDestroy()
    {
        CollisionEnter =  null;
    }
}