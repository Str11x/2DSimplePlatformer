using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform CurrentTransform { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            CurrentTransform = collision.collider.transform;
            Debug.Log(CurrentTransform.position);
            Selection(interactable);
        }
    }

    private void Selection(IInteractable interactable)
    {
        if (interactable is Coin coin)
        {
            coin.Interact();
        }
    }
}