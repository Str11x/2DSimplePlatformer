using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private Enemy _template;
    [SerializeField] private SpawnSign _sign;
    [SerializeField] private ParticleSystem _startParticle;
    [SerializeField] private GameObject _endParticle;
    [SerializeField] private Effects _particle;

    private Transform[] _points;
    private int _currentPoint;
    private WaitForSeconds _spawnerPauseTime = new WaitForSeconds(3);
    private WaitForSeconds _showSpawnerTime = new WaitForSeconds(5);

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

            var startEffect = Instantiate(_startParticle, _points[_currentPoint].transform.position, Quaternion.identity);
            Destroy(startEffect.gameObject, 1);

            var lastSpawn = Instantiate(_sign, _points[_currentPoint].transform.position, Quaternion.identity);
            Destroy(lastSpawn.gameObject, 5);

            var effect = Instantiate(_particle, _points[_currentPoint].transform.position, Quaternion.identity);
            effect.transform.SetParent(lastSpawn.transform);
            Destroy(effect.gameObject, 5);

            yield return _showSpawnerTime;

            var endEffect = Instantiate(_endParticle, _points[_currentPoint].transform.position, Quaternion.identity);
            Destroy(endEffect.gameObject, 2);

            Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);

            yield return _spawnerPauseTime;
        }
    }
}
