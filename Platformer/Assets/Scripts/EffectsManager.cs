using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ParticleSystem _death;
    [SerializeField] private EnemyInstance _enemyInstance;
    [SerializeField] private ParticleSystem _instanceExplosion;
    [SerializeField] private SpawnSign _sign;
    [SerializeField] private SpriteRenderer _signRenderer;
    [SerializeField] private ParticleSystem _spawnFlame;
    [SerializeField] private ParticleSystem _electricity;
    [SerializeField] private ParticleSystem _shine;
    [SerializeField] private CoinInstance _coinInstance;
    [SerializeField] private ParticleSystem _instance;
    [SerializeField] private ParticleSystem _pickUpCoin;

    private int _lifetimeSpawnSign = 5;
    private Interactor _coinInteractor;

    private void Start()
    {
        GameEvents.Current.OnPlayerDestroy += Death;
        GameEvents.Current.OnEnemyEffectsInstance += InstanceEnemyEffects;
        GameEvents.Current.OnEnemyInstance += InstanceEnemy;
        GameEvents.Current.OnCoinInstance += InstanceCoin;
        GameEvents.Current.OnPickupCoin += PickUpCoin;
        
        _spawnFlame.transform.SetParent(_sign.transform);
        _coinInteractor = _player.Interactor;
    }

    private void OnDisable()
    {
        GameEvents.Current.OnPlayerDestroy -= Death;
        GameEvents.Current.OnPlayerDestroy -= InstanceEnemyEffects;
        GameEvents.Current.OnEnemyInstance -= InstanceEnemy;
        GameEvents.Current.OnCoinInstance -= InstanceCoin;
        GameEvents.Current.OnPickupCoin -= PickUpCoin;
    }

    private void Death()
    {
        _death.transform.position = _player.transform.position;
        _death.Play();
    }

    private void InstanceEnemyEffects()
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

    private void InstanceEnemy()
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

    private void InstanceCoin()
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