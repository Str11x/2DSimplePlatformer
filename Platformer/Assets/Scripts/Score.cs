using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _score = 0;

    void Start()
    {
        _scoreText.text = _score.ToString() + "POINTS";
    }
}
