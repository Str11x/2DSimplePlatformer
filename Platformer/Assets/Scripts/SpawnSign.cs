using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSign : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent = collision.transform;
    }
}
