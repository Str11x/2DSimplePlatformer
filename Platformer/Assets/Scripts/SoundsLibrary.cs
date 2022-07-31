using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsLibrary : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Score _scoreCount;
    [SerializeField] private EnemySpawner _spawner;

    [SerializeField] private AudioSource _pickupCoin;
    [SerializeField] private AudioSource _instanceSpawnWhosh;
    [SerializeField] private AudioSource _instanceEnemy;
    [SerializeField] private AudioSource _playerHit;
    [SerializeField] private AudioSource _enemyHit;
    [SerializeField] private AudioSource _death;

    private Health _health;

    void Start()
    {
        _health = _player.GetComponent<Health>();

        _health.PlayerKilled += DestroyPlayer;
        _player.TookDamageFromEnemy += HitFromEnemy;
        _scoreCount.CoinPickuped += PickupCoin;
        _player.DamageDone += HitFromPlayer;
        _spawner.SpawnEnemyEffects += CreateSpawn;
        _spawner.SpawnEnemy += SpawnEnemy;
    }

    private void OnDisable()
    {
        _health.PlayerKilled -= DestroyPlayer;
        _player.TookDamageFromEnemy -= HitFromEnemy;
        _scoreCount.CoinPickuped -= PickupCoin;
        _player.DamageDone -= HitFromPlayer;
        _spawner.SpawnEnemyEffects -= CreateSpawn;
        _spawner.SpawnEnemy += SpawnEnemy; 
    }

    private void PickupCoin()
    {
        _pickupCoin.Play();
    }

    private void CreateSpawn()
    {
        _instanceSpawnWhosh.Play();
    }

    private void SpawnEnemy()
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