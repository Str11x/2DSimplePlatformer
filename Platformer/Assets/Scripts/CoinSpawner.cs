using System;
using System.Collections;
using UnityEngine;

public class CoinSpawner : Spawner
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Coin _template;

    [SerializeField] private ParticleSystem _instance;
   
    private int _currentPoint;

    public event Action SpawnedCoin;

    protected override IEnumerator SpawnInRandomPlace()
    {
        bool isSpawnerEnable = true;

        while (isSpawnerEnable)
        {
            _currentPoint = UnityEngine.Random.Range(0, Points.Length);
            CurrentPosition = Points[_currentPoint].transform.position;

            SpawnedCoin?.Invoke();
            SpawnCoin();

            Instantiate(_template, Points[_currentPoint].transform.position, Quaternion.identity);

            yield return SpawnerPauseTime;
        }
    }

    private void SpawnCoin()
    {
        _instance.transform.position = CurrentPosition;
        _instance.Play();
    }
}