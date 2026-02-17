using System;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PanelsCollisionMediator _panelsCollisionMediator;
    [SerializeField] private CubesSpawner _cubesSpawner;

    private CubesLifecycleBehavior _cubesLifecycleBehavior;
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _cubesLifecycleBehavior = gameObject.AddComponent<CubesLifecycleBehavior>();
        _colorChanger = new ColorChanger();
    }


    private void OnEnable()
    {
        _panelsCollisionMediator.CubeCollisionEntered += _cubesLifecycleBehavior.StartLifecycle;
        _cubesLifecycleBehavior.CubeLifetimeEnded += _cubesSpawner.ReleasedToPool;
        _cubesLifecycleBehavior.CubeLifetimeStarted += _colorChanger.UpdateColor;

        _cubesSpawner.CubeSpawned += _colorChanger.SetBaseColor;
    }

    private void OnDisable()
    {
        
        _panelsCollisionMediator.CubeCollisionEntered -= _cubesLifecycleBehavior.StartLifecycle;
        _cubesLifecycleBehavior.CubeLifetimeEnded -= _cubesSpawner.ReleasedToPool;
        _cubesLifecycleBehavior.CubeLifetimeStarted -= _colorChanger.UpdateColor;

        _cubesSpawner.CubeSpawned -= _colorChanger.SetBaseColor;
    }
}