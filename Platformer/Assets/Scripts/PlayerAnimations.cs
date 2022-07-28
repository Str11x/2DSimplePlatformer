using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _speed = Animator.StringToHash("Speed");
    private int _jump = Animator.StringToHash("IsJump");
    private int _healthAnimation = Animator.StringToHash("Health");

    private float _startMovement = 3;
    private int _stopMovement = 0;

    private void Start()
    {
        GameEvents.Current.OnPlayerDestroy += Death;
    }

    private void Update()
    {
        HorizontalMovement();
        Jump();
    }

    private void HorizontalMovement()
    {
        if (Input.GetButton("Horizontal"))
        {
            _animator.SetFloat(_speed, _startMovement);
        }
        else
        {
            _animator.SetFloat(_speed, _stopMovement);
        }
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            _animator.SetBool(_jump, true);
        }
        else
        {
            _animator.SetBool(_jump, false);
        }
    }

    private void Death()
    {
        _animator.SetInteger(_healthAnimation, 0);
    }
}