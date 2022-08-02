using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform SpawnPoints;

    protected Transform[] Points;
    protected WaitForSeconds SpawnerPauseTime = new WaitForSeconds(3);

    public Vector3 CurrentPosition { get; protected set; }

    protected void Start()
    {
        Points = new Transform[SpawnPoints.childCount];

        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = SpawnPoints.GetChild(i);
        }

        StartCoroutine(CreateInRandomPlace());
    }

    protected virtual IEnumerator CreateInRandomPlace()
    {
         yield return SpawnerPauseTime;
    }
}