using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Effects _deathEffect;
    [SerializeField] private Heart _heart;
    [SerializeField] private Transform _heartPoints;

    private List<Heart> _rendererHearts = new List<Heart>();

    private int _healthAnimation = Animator.StringToHash("Health");
    private int _health = 3;
    private int _spawnEnvironmentLayer = 6;
    private int _timeToDeathDestroy = 1;
    private int _timeToLevelRestart = 3;
    private int _numberOfSceneRestart = 0;   

    public bool IsDeath { get; private set; } = false;

    private void Start()
    {
        for (int i = 0; i < _health; i++)
        {
            _rendererHearts.Add(Instantiate(_heart, _heartPoints.GetChild(i).transform.position, Quaternion.identity));
        }
    }

    public void TakeDamage()
    {
        if(_health > 1)
        {
            _health--;
            StartCoroutine(MakeUnattackable());

            Destroy(_rendererHearts[_rendererHearts.Count - 1].gameObject);
            _rendererHearts.RemoveAt(_rendererHearts.Count - 1);
        }
        else
        {
            _animator.SetInteger(_healthAnimation, 0);
            IsDeath = true;
            gameObject.layer = _spawnEnvironmentLayer;

            Destroy(_rendererHearts[_rendererHearts.Count - 1].gameObject);
            _rendererHearts.RemoveAt(_rendererHearts.Count - 1);

            var deathEffect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(deathEffect.gameObject, _timeToDeathDestroy);

            Invoke("Restart", _timeToLevelRestart);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(_numberOfSceneRestart);
    }

    private IEnumerator MakeUnattackable()
    {
        while (true)
        {
            gameObject.layer = _spawnEnvironmentLayer;

            yield return new WaitForSeconds(3);

            gameObject.layer = 7;
            break;
        }
    }
}