using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class Interactor : MonoBehaviour
{
    public Transform CurrentTransform { get; private set; }
    public bool IsCauseDamage { get; private set; }

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
            coin.Interact();       
        else if (interactable is Enemy enemy && IsCauseDamage)           
            enemy.Interact();          
    }

    private void CalculateDropDitance(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            if (point.normal.y < 0 || point.normal.x > 0.5f || point.normal.x < -0.5f)
            {
                IsCauseDamage = true;
            }
        }
    }
}