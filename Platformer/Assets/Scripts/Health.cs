using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(PlayerController))]
public class Health : MonoBehaviour
{
    [SerializeField] private Heart _heart;
    [SerializeField] private Transform _heartPoints;
    [SerializeField] private ParticleSystem _death;

    private List<Heart> _rendererHearts = new List<Heart>();
    private WaitForSeconds _unattackableTime = new WaitForSeconds(3);
    private PlayerController _player;

    private int _health = 3;
    private int _spawnEnvironmentLayer = 6;
    private int _PlayerLayer = 7;
    private int _unAttackableLayer = 10;
    private int _timeToLevelRestart = 3;
    private int _numberOfSceneRestart = 0;
    private bool _isUnattackable = true;

    public event Action PlayerKilled;

    public bool IsDeath { get; private set; } = false;

    private void Start()
    {
        _player = GetComponent<PlayerController>();

        _player.TookDamageFromEnemy += TakeDamage;

        for (int i = 0; i < _health; i++)
        {
            _rendererHearts.Add(Instantiate(_heart, _heartPoints.GetChild(i).transform.position, Quaternion.identity));
        }
    }

    private void OnDisable()
    {
        _player.TookDamageFromEnemy -= TakeDamage;
    }

    private void TakeDamage()
    {
        if(_health > 1)
        {
            _health--;
            _isUnattackable = true;
            StartCoroutine(MakeUnattackableForWhile());

            Destroy(_rendererHearts[_rendererHearts.Count - 1].gameObject);
            _rendererHearts.RemoveAt(_rendererHearts.Count - 1);
        }
        else
        {  
            IsDeath = true;
            gameObject.layer = _spawnEnvironmentLayer;

            PlayerKilled?.Invoke();
            PlayDeathEffect();

            Destroy(_rendererHearts[_rendererHearts.Count - 1].gameObject);

            Invoke(nameof(Restart), _timeToLevelRestart);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(_numberOfSceneRestart);
    }

    private IEnumerator MakeUnattackableForWhile()
    {
        while (_isUnattackable)
        {
            gameObject.layer = _unAttackableLayer;

            yield return _unattackableTime;

            gameObject.layer = _PlayerLayer;

            _isUnattackable = false;
        }
    }

    private void PlayDeathEffect()
    {
        _death.transform.position = _player.transform.position;
        _death.Play();
    }
}