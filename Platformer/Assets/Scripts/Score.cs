using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _score = 0;

    private void Start()
    {
        GameEvents.Current.OnPickupCoin += AddPoint;

        _scoreText.text = _score.ToString() + " POINTS";
    }

    private void OnDisable()
    {
        GameEvents.Current.OnPickupCoin -= AddPoint;
    }

    private void AddPoint()
    {
        _score++;
        _scoreText.text = _score.ToString() + " POINTS";
    }
}
