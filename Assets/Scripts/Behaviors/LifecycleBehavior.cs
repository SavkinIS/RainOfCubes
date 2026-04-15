using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifecycleBehavior : MonoBehaviour
{
    private const float TickInterval = 0.2f;

    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private bool _isTicking = true;
    private float _lifetime;
    private Coroutine _lifecycleCoroutine;

    public event Action LifetimeEnded;
    public event Action<float, float> LifetimeChanged;

    public void StartLifecycle()
    {
        _lifetime = Random.Range(_minLifetime, _maxLifetime + 1);
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (_isTicking)
        {
            _lifetime -= TickInterval;
            
            LifetimeChanged?.Invoke(_lifetime, _maxLifetime);
            
            if (_lifetime <= 0f)
            {
                LifetimeEnded?.Invoke();
                yield break;
            }

            yield return new WaitForSeconds(TickInterval);
        }
    }
}