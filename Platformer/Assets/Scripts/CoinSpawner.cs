using System;
using System.Collections;
using UnityEngine;

public class CoinSpawner : Spawner
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Coin _template;

    [SerializeField] private ParticleSystem _instance;
    [SerializeField] private ParticleSystem _pickUpCoin;
   
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
            SpawnCoin();

            Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);

            yield return _spawnerPauseTime;
        }
    }
    private void SpawnCoin()
    {
        _instance.transform.position = CurrentPosition;
        _instance.Play();
    }

    public void PickUpCoinEffect()
    {
        _pickUpCoin.transform.position = _player.Interactor.CurrentTransform.position;
        _pickUpCoin.Play();
    }
}