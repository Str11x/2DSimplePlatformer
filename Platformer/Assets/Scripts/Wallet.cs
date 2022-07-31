using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] Score _scoreCounter;

    public void AddCoin()
    {
        _scoreCounter.PickupCoin();
    }
}