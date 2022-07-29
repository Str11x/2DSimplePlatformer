using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;

    private void Awake()
    {
        Current = this;
    }

    public event Action OnPickupCoin;

    public event Action OnTakeDamageFromEnemy;

    public event Action OnTakeDamageFromPlayer;

    public event Action OnPlayerDestroy;

    public event Action OnEnemyEffectsInstance;

    public event Action OnEnemyInstance;

    public event Action OnCoinInstance;

    public void PickupCoin()
    {
        OnPickupCoin?.Invoke();
    }

    public void TakeDamageFromEnemy()
    {
        OnTakeDamageFromEnemy?.Invoke();
    }

    public void DestroyPlayer()
    {
        OnPlayerDestroy?.Invoke();
    }

    public void InstanceEnemyEffects()
    {
        OnEnemyEffectsInstance?.Invoke();
    }

    public void InstanceEnemy()
    {
        OnEnemyInstance?.Invoke();
    }

    public void InstanceCoin()
    {
        OnCoinInstance?.Invoke();
    }

    public void TakeDamageFromPlayer()
    {
        OnTakeDamageFromPlayer?.Invoke();
    }
}