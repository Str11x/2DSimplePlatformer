using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IInteractable    
{ 
    public void Interact()
    {
        Destroy(this.gameObject);
        GameEvents.Current.PickupCoin();
    }
}
