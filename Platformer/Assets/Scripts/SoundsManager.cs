using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _pickupCoin;

    void Start()
    {
        GameEvents.Current.OnPickupCoin += PickupCoin;
    }
    private void PickupCoin()
    {
        _pickupCoin.Play();
    }

    private void OnDisable()
    {
        GameEvents.Current.OnPickupCoin -= PickupCoin;
    }
}
