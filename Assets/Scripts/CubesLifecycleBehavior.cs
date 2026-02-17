using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesLifecycleBehavior : MonoBehaviour
{
    private readonly int _minLifetime = 2;
    private readonly int _maxLifetime = 5;
    private List<Lifetime> _lifetimeCubes;
    private List<Lifetime> _forRemoveCubes;

    private void Awake()
    {
        _lifetimeCubes = new();
        _forRemoveCubes = new();
    }

    public event Action<Cube> CubeLifetimeEnded;
    public event Action<Cube> CubeLifetimeStarted;

    public void StartLifecycle(Cube cube)
    {
        if (!cube.TryGetComponent(out Lifetime lifetime))
        {
            lifetime = cube.AddComponent<Lifetime>();
            lifetime.Time = Random.Range(_minLifetime, _maxLifetime + 1);
            lifetime.Cube = cube;
            _lifetimeCubes.Add(lifetime);
            CubeLifetimeStarted?.Invoke(cube);
        }
    }

    public void Update()
    {
        float timeElapsed = Time.deltaTime;

        foreach (var lifetime in _lifetimeCubes)
        {
            lifetime.Time -= timeElapsed;

            if (lifetime.Time <= 0)
            {
                _forRemoveCubes.Add(lifetime);
            }
        }

        if (_forRemoveCubes.Count > 0)
        {
            foreach (var lifetime in _forRemoveCubes)
            {
                _lifetimeCubes.Remove(lifetime);
                Cube cube = lifetime.Cube;
                Destroy(lifetime);
                CubeLifetimeEnded?.Invoke(cube);
            }

            _forRemoveCubes.Clear();
        }
    }

    private void OnDestroy()
    {
        CubeLifetimeEnded = null;
        CubeLifetimeStarted = null;
    }
}