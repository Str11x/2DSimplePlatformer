using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;

    private void Awake()
    {
        Current = this;
    }

    public event Action OnPickupCoin;

    public void PickupCoin()
    {
        OnPickupCoin?.Invoke();
    }
}
