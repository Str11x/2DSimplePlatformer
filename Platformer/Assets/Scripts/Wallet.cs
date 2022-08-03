using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Wallet : MonoBehaviour
{
    [SerializeField] Score _scoreCounter;
    [SerializeField] private ParticleSystem _pickUpCoin;

    private PlayerMovement _player;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
    }

    public void AddCoin()
    {
        _scoreCounter.PickupCoin();
        PickUpCoinEffect();
    }

    public void PickUpCoinEffect()
    {
        _pickUpCoin.transform.position = _player.Interactor.CurrentTransform.position;
        _pickUpCoin.Play();
    }
}