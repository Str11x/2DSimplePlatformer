using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Wallet))]
public class Interactor : MonoBehaviour
{
    [SerializeField] private CoinSpawner coinSpawner;

    private float _minDistanceNormalXLeft = -0.5f;
    private float _minDistanceNormalXRight = 0.5f;

    private Wallet _wallet;
    public Transform CurrentTransform { get; private set; }
    public bool IsCauseDamage { get; private set; }

    private void Start()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsCauseDamage = false;

        if (collision.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            CurrentTransform = collision.collider.transform;

            CalculateDropDitance(collision);
            SelectInteract(interactable);
        }
    }

    private void SelectInteract(IInteractable interactable)
    {
        if (interactable is Coin coin)
        {
            _wallet.AddCoin();
            coin.Interact();
        }                  
        else if (interactable is Enemy enemy && IsCauseDamage)
        {
            enemy.Interact();
        }                                    
    }

    private void CalculateDropDitance(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            if (point.normal.y < 0 || point.normal.x > _minDistanceNormalXRight || point.normal.x < _minDistanceNormalXLeft)
            {
                IsCauseDamage = true;
            }
        }
    }
}