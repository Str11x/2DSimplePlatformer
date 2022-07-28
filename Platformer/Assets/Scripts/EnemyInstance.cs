using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private Enemy _template;

    private Transform[] _points;
    private int _currentPoint;
    private WaitForSeconds _spawnerPauseTime = new WaitForSeconds(3);
    private WaitForSeconds _showSpawnerTime = new WaitForSeconds(5);

    public Vector3 CurrentPosition { get; private set;  }

    private void Start()
    {
        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _spawnPoints.GetChild(i);
        }

        StartCoroutine(CreateInRandomPlace());
    }

    private IEnumerator CreateInRandomPlace()
    {
        bool isSpawnerEnable = true;

        while (isSpawnerEnable)
        {
            _currentPoint = Random.Range(0, _points.Length);
            CurrentPosition = _points[_currentPoint].transform.position;

            GameEvents.Current.InstanceEnemyEffects();

            yield return _showSpawnerTime;

            GameEvents.Current.InstanceEnemy();

            Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);

            yield return _spawnerPauseTime;
        }
    }
}