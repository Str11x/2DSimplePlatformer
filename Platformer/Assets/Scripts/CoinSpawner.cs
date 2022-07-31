using System;
using System.Collections;
using UnityEngine;

public class CoinSpawner : Spawner
{
    [SerializeField] private Coin _template;

    private int _currentPoint;

    public event Action CreatedCoin;

    protected override IEnumerator CreateInRandomPlace()
    {
        bool isSpawnerEnable = true;

        while (isSpawnerEnable)
        {
            _currentPoint = UnityEngine.Random.Range(0, _points.Length);
            CurrentPosition = _points[_currentPoint].transform.position;

            CreatedCoin?.Invoke();

            Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);

            yield return _spawnerPauseTime;
        }
    }
}