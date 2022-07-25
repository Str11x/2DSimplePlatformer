using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInstance : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private Coin _template;
    [SerializeField] private ParticleSystem _startParticle;
    [SerializeField] private GameObject _endParticle;
    [SerializeField] private ParticleSystem _pickupEffect;

    private Transform[] _points;
    private int _currentPoint;
    private WaitForSeconds _spawnerPauseTime = new WaitForSeconds(3);
    private Interactor coinInteractor;

    private void Start()
    {
        coinInteractor = _player.GetComponent<Interactor>();

        GameEvents.Current.OnPickupCoin += MadePickupEffect;

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

            Instantiate(_template, _points[_currentPoint].transform.position, Quaternion.identity);

            yield return _spawnerPauseTime;
        }
    }

    private void MadePickupEffect()
    {
        _pickupEffect.transform.position = coinInteractor.CurrentTransform.position;

        _pickupEffect.Play();
    }

    private void OnDisable()
    {
        GameEvents.Current.OnPickupCoin -= MadePickupEffect;
    }
}
