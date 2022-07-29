using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IInteractable    
{ 
    public void Interact()
    {
        GameEvents.Current.PickupCoin();
        Destroy(this.gameObject);    
    }
}
