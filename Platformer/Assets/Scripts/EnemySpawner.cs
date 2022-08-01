using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Enemy _template;
    [SerializeField] private PlayerController _player;
    [SerializeField] private SpawnSign _sign;
    [SerializeField] private SpriteRenderer _signRenderer;

    [SerializeField] private ParticleSystem _instanceExplosion;
    [SerializeField] private ParticleSystem _spawnFlame;
    [SerializeField] private ParticleSystem _electricity;
    [SerializeField] private ParticleSystem _shine;

    private int _currentPoint;
    private WaitForSeconds _showSpawnerTime = new WaitForSeconds(5);
    private int _lifetimeSpawnSign = 5;

    public event Action SpawnEnemyEffects;
    public event Action SpawnEnemy;

    private void Awake()
    {
        _spawnFlame.transform.SetParent(_sign.transform);
    }
    protected override IEnumerator CreateInRandomPlace()
    {
        bool isSpawnerEnable = true;

        while (isSpawnerEnable)
        {
            _currentPoint = UnityEngine.Random.Range(0, _points.Length);
            CurrentPosition = _points[_currentPoint].transform.position;

            SpawnEnemyEffects?.Invoke();
            SpawnEffects();

            yield return _showSpawnerTime;

            SpawnEnemy?.Invoke();
            Spawn();

            var enemy = Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);
            ReportPlayerPosition(enemy);

            yield return _spawnerPauseTime;
        }
    }

    private void ReportPlayerPosition(Enemy enemy)
    {
        enemy.SetPlayerPosition(_player);
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

    private void Spawn()
    {
        _shine.transform.position = CurrentPosition;
        _shine.Play();
    }
}