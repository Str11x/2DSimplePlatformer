using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsLibrary : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Score _scoreCount;
    [SerializeField] private SpawnSign _sign;
    [SerializeField] private EnemySpawner _enemyInstance;
    [SerializeField] private SpriteRenderer _signRenderer;
    [SerializeField] private CoinSpawner _coinInstance;

    [SerializeField] private ParticleSystem _death;   
    [SerializeField] private ParticleSystem _instanceExplosion;     
    [SerializeField] private ParticleSystem _spawnFlame;
    [SerializeField] private ParticleSystem _electricity;
    [SerializeField] private ParticleSystem _shine; 
    [SerializeField] private ParticleSystem _instance;
    [SerializeField] private ParticleSystem _pickUpCoin;

    private Health _health;  
    private Interactor _coinInteractor;

    private int _lifetimeSpawnSign = 5;

    private void Start()
    {
        _health = _player.GetComponent<Health>();

        _health.PlayerKilled += Death;
        _scoreCount.CoinPickuped += PickUpCoin;
        _enemyInstance.SpawnEnemyEffects += SpawnEnemyEffects;
        _enemyInstance.SpawnEnemy += SpawnEnemy;
        _coinInstance.CreatedCoin += SpawnCoin;
        
        _spawnFlame.transform.SetParent(_sign.transform);
        _coinInteractor = _player.Interactor;
    }

    private void OnDisable()
    {
        _health.PlayerKilled -= Death;
        _scoreCount.CoinPickuped -= PickUpCoin;
        _enemyInstance.SpawnEnemyEffects -= SpawnEnemyEffects;
        _enemyInstance.SpawnEnemy += SpawnEnemy;
        _coinInstance.CreatedCoin -= SpawnCoin;
    }

    private void Death()
    {
        _death.transform.position = _player.transform.position;
        _death.Play();
    }

    private void SpawnEnemyEffects()
    {
        _instanceExplosion.transform.position = _enemyInstance.CurrentPosition;
        _instanceExplosion.Play();

        _electricity.transform.SetParent(_sign.transform);
        _electricity.Play();

        _sign.transform.position = _enemyInstance.CurrentPosition;
        _signRenderer.enabled = true;

        _spawnFlame.transform.SetParent(_sign.transform);
        _spawnFlame.Play();

        Invoke(nameof(DisableSpawnSign), _lifetimeSpawnSign);
    }

    private void SpawnEnemy()
    {
        _shine.transform.position = _enemyInstance.CurrentPosition;
        _shine.Play();
    }

    private void DisableSpawnSign()
    {
        _signRenderer.enabled = false;
        _spawnFlame.Stop();
        _electricity.Stop();
    }

    private void SpawnCoin()
    {
        _instance.transform.position = _coinInstance.CurrentPosition;
        _instance.Play();
    }

    private void PickUpCoin()
    {
        _pickUpCoin.transform.position = _coinInteractor.CurrentTransform.position;
        _pickUpCoin.Play();
    }
}