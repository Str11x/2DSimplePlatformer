using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private Heart _heart;
    [SerializeField] private Transform _heartPoints;

    private List<Heart> _rendererHearts = new List<Heart>();

    private int _health = 3;
    private int _spawnEnvironmentLayer = 6;
    private int _PlayerLayer = 7;
    private int _UnAttackableLayer = 10;
    private int _timeToLevelRestart = 3;
    private int _numberOfSceneRestart = 0;
    private bool _isUnattackable = true;

    public bool IsDeath { get; private set; } = false;

    private void Start()
    {
        GameEvents.Current.OnTakeDamageFromEnemy += TakeDamage;

        for (int i = 0; i < _health; i++)
        {
            _rendererHearts.Add(Instantiate(_heart, _heartPoints.GetChild(i).transform.position, Quaternion.identity));
        }
    }

    private void OnDisable()
    {
        GameEvents.Current.OnTakeDamageFromEnemy -= TakeDamage;
    }

    private void TakeDamage()
    {
        if(_health > 1)
        {
            _health--;
            _isUnattackable = true;
            StartCoroutine(MakeUnattackable());

            Destroy(_rendererHearts[_rendererHearts.Count - 1].gameObject);
            _rendererHearts.RemoveAt(_rendererHearts.Count - 1);
        }
        else
        {  
            IsDeath = true;
            gameObject.layer = _spawnEnvironmentLayer;

            GameEvents.Current.DestroyPlayer();

            Destroy(_rendererHearts[_rendererHearts.Count - 1].gameObject);

            Invoke(nameof(Restart), _timeToLevelRestart);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(_numberOfSceneRestart);
    }

    private IEnumerator MakeUnattackable()
    {
        while (_isUnattackable)
        {
            gameObject.layer = _UnAttackableLayer;

            yield return new WaitForSeconds(3);

            gameObject.layer = _PlayerLayer;

            _isUnattackable = false;
        }
    }
}