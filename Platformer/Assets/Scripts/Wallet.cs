using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] Score _scoreCounter;
    [SerializeField] CoinSpawner _coinSpawner;

    public void AddCoin()
    {
        _scoreCounter.PickupCoin();
        _coinSpawner.PickUpCoinEffect();
    }
}