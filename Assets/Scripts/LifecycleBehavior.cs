using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifecycleBehavior : MonoBehaviour
{
    private const float TickInterval = 1f;

    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private bool _isTicking = true;
    private float _lifetime;
    private Coroutine _lifecycleCoroutine;

    public event Action CubeLifetimeEnded;

    public void StartLifecycle()
    {
        _lifetime = Random.Range(_minLifetime, _maxLifetime + 1);
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(TickInterval);

        while (_isTicking)
        {
            _lifetime -= TickInterval;

            if (_lifetime <= 0)
            {
                CubeLifetimeEnded?.Invoke();
                break;
            }

            yield return null;
        }
    }
}