using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Enemy _template;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private SpawnSign _sign;
    [SerializeField] private SpriteRenderer _signRenderer;

    [SerializeField] private ParticleSystem _instanceExplosion;
    [SerializeField] private ParticleSystem _spawnFlame;
    [SerializeField] private ParticleSystem _electricity;
    [SerializeField] private ParticleSystem _shine;

    private int _currentPoint;
    private WaitForSeconds _showSpawnerTime = new WaitForSeconds(5);
    private int _lifetimeSpawnSign = 5;

    public event Action SpawnedEnemyEffects;
    public event Action SpawnedEnemy;

    private void Awake()
    {
        _spawnFlame.transform.SetParent(_sign.transform);
    }
    protected override IEnumerator SpawnInRandomPlace()
    {
        bool isSpawnerEnable = true;

        while (isSpawnerEnable)
        {
            _currentPoint = UnityEngine.Random.Range(0, Points.Length);
            CurrentPosition = Points[_currentPoint].transform.position;

            SpawnedEnemyEffects?.Invoke();
            SpawnEffects();

            yield return _showSpawnerTime;

            SpawnedEnemy?.Invoke();
            SpawnShine();

            var enemy = Instantiate(_template, Points[_currentPoint].transform.position, Quaternion.identity);
            ReportPlayerPosition(enemy);

            yield return SpawnerPauseTime;
        }
    }

    private void ReportPlayerPosition(Enemy enemy)
    {
        enemy.SetTargetToPursue(_player);
    }

    private void SpawnEffects()
    {
        _instanceExplosion.transform.position = CurrentPosition;
        _instanceExplosion.Play();

        _electricity.transform.SetParent(_sign.transform);
        _electricity.Play();

        _sign.transform.position = CurrentPosition;
        _signRenderer.enabled = true;

        _spawnFlame.transform.SetParent(_sign.transform);
        _spawnFlame.Play();

        Invoke(nameof(DisableSpawnSign), _lifetimeSpawnSign);
    }

    private void DisableSpawnSign()
    {
        _signRenderer.enabled = false;
        _spawnFlame.Stop();
        _electricity.Stop();
    }

    private void SpawnShine()
    {
        _shine.transform.position = CurrentPosition;
        _shine.Play();
    }
}