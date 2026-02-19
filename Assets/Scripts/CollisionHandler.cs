using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool _isCollisioned;
    public event Action CollisionEnter;

    private void OnCollisionEnter(Collision other)
    {
        if (_isCollisioned == false)
        {
            if (other.transform.TryGetComponent(out Platform platform))
            {
                _isCollisioned = true;
                CollisionEnter?.Invoke();
            }
        }
    }
    
    public void ResetCollision()
    {
        _isCollisioned = false;
    }
}