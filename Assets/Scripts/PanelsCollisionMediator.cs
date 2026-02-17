using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelsCollisionMediator : MonoBehaviour
{
    [SerializeField] private List<CollisionHandler> _collisionDetectors;

    public event Action<Cube> CubeCollisionEntered;

    private void OnEnable()
    {
        foreach (CollisionHandler collisionDetector in _collisionDetectors)
        {
            collisionDetector.CollisionEnter += CubeCollisionEntered;
        }
    }
    
    private void OnDisable()
    {
        foreach (CollisionHandler collisionDetector in _collisionDetectors)
        {
            collisionDetector.CollisionEnter -= CubeCollisionEntered;
        }
    }

    private void OnDestroy()
    {
        CubeCollisionEntered = null;
    }
}