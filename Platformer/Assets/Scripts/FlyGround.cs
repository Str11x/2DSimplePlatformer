using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyGround : MonoBehaviour
{
    [SerializeField] private Transform _flyBackground;
    [SerializeField] private int _animationDuration = 15;
    [SerializeField] private float _forceMovement = 0.3f;
    [SerializeField] private int _numberOfMovePoint = 3;

    private Vector3[] _pathPoints;
    private Transform [] _islands = new Transform[0];
    private Tween _flyAnimation;

    private void Start()
    {
        _islands = new Transform[transform.childCount];

        for(int i = 0; i < _islands.Length; i++)
        {
            _islands[i] = _flyBackground.GetChild(i);
        }

        for (int i = 0; i < _islands.Length; i++)
        {
            Vector3[] vectorDirections = new[]
            {
                new Vector3(_islands[i].transform.position.x, _islands[i].transform.position.y + _forceMovement),
                new Vector3(_islands[i].transform.position.x, _islands[i].transform.position.y - _forceMovement),
                new Vector3(_islands[i].transform.position.x - _forceMovement, _islands[i].transform.position.y),
                new Vector3(_islands[i].transform.position.x + _forceMovement, _islands[i].transform.position.y)
            };

            _pathPoints = vectorDirections;

            Vector3 initialPosition = _islands[i].transform.position;


            for (int vectorIndex = 0; vectorIndex < _numberOfMovePoint; vectorIndex++)
            {
                if (vectorIndex == _numberOfMovePoint - 1)
                {
                    _pathPoints[vectorIndex] = initialPosition;
                    break;
                }
                    
                int nextPointToMove = Random.Range(0, vectorDirections.Length);

                _pathPoints[vectorIndex] = vectorDirections[nextPointToMove];
            }

            _flyAnimation = _islands[i].DOPath(_pathPoints, _animationDuration, PathType.CatmullRom).SetAutoKill(false).SetOptions(true);
            _flyAnimation.SetEase(Ease.Linear).SetLoops(-1);
        }
    }
}