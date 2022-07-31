using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform _spawnPoints;

    protected Transform[] _points;
    protected WaitForSeconds _spawnerPauseTime = new WaitForSeconds(3);

    public Vector3 CurrentPosition { get; protected set; }

    protected void Start()
    {
        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _spawnPoints.GetChild(i);
        }

        StartCoroutine(CreateInRandomPlace());
    }

    protected virtual IEnumerator CreateInRandomPlace()
    {
         yield return _spawnerPauseTime;
    }
}
