using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Enemy _template;
    [SerializeField] private PlayerController _player;

    private int _currentPoint;
    private WaitForSeconds _showSpawnerTime = new WaitForSeconds(5);

    public event Action SpawnEnemyEffects;
    public event Action SpawnEnemy;

    protected override IEnumerator CreateInRandomPlace()
    {
        bool isSpawnerEnable = true;

        while (isSpawnerEnable)
        {
            _currentPoint = UnityEngine.Random.Range(0, _points.Length);
            CurrentPosition = _points[_currentPoint].transform.position;

            SpawnEnemyEffects?.Invoke();

            yield return _showSpawnerTime;

            SpawnEnemy?.Invoke();

            var enemy = Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);
            ReportPlayerPosition(enemy);

            yield return _spawnerPauseTime;
        }
    }

    private void ReportPlayerPosition(Enemy enemy)
    {
        enemy.SetPlayerPosition(_player);
    }
}