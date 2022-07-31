using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public event Action CoinPickuped;

    private int _score = 0;

    private void Start()
    {
        CoinPickuped += AddPoint;

        ShowPoints();
    }

    private void OnDisable()
    {
        CoinPickuped -= AddPoint;
    }

    private void AddPoint()
    {
        _score++;

        ShowPoints();
    }

    private void ShowPoints()
    {
        _scoreText.text = _score.ToString() + " POINTS";
    }

    public void PickupCoin()
    {
        CoinPickuped?.Invoke();
    }
}