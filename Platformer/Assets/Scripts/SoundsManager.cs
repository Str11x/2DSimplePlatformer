using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _pickupCoin;
    [SerializeField] private AudioSource _instanceSpawnWhosh;
    [SerializeField] private AudioSource _instanceEnemy;
    [SerializeField] private AudioSource _playerHit;
    [SerializeField] private AudioSource _enemyHit;
    [SerializeField] private AudioSource _death;

    void Start()
    {
        GameEvents.Current.OnPickupCoin += PickupCoin;
        GameEvents.Current.OnEnemyEffectsInstance += InstanceSpawn;
        GameEvents.Current.OnEnemyInstance += InstanceEnemy;
        GameEvents.Current.OnTakeDamageFromPlayer += HitFromPlayer;
        GameEvents.Current.OnTakeDamageFromEnemy += HitFromEnemy;
        GameEvents.Current.OnPlayerDestroy += DestroyPlayer;
    }

    private void OnDisable()
    {
        GameEvents.Current.OnPickupCoin -= PickupCoin;
        GameEvents.Current.OnEnemyEffectsInstance -= InstanceSpawn;
        GameEvents.Current.OnEnemyInstance -= InstanceEnemy;
        GameEvents.Current.OnTakeDamageFromPlayer -= HitFromPlayer;
        GameEvents.Current.OnTakeDamageFromEnemy -= HitFromEnemy;
        GameEvents.Current.OnPlayerDestroy -= DestroyPlayer;
    }

    private void PickupCoin()
    {
        _pickupCoin.Play();
    }

    private void InstanceSpawn()
    {
        _instanceSpawnWhosh.Play();
    }

    private void InstanceEnemy()
    {
        _instanceEnemy.Play();
    }

    private void HitFromPlayer()
    {
        _playerHit.Play();
    }

    private void HitFromEnemy()
    {
        _enemyHit.Play();
    }

    private void DestroyPlayer()
    {
        _death.Play();
    }
}