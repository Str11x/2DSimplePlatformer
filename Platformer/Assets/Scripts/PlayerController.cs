using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _jumpForce = 15;
    [SerializeField] private float maximumNearDistance = 0.99f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Health _health;  

    private float _movement;
    private bool _isJump;
    private bool _isGrounded = true;
    private bool _isCanDoubleJump;
    private bool _readyToNextJump = true;


    void Start()
    {   
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();      
    }

    private void Update()
    {
            _movement = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && _readyToNextJump)
                _isJump = true;

            if (_movement < 0 && _health.IsDeath == false)
                _spriteRenderer.flipX = true;
            else if (_movement > 0 && _health.IsDeath == false)
                _spriteRenderer.flipX = false; 
    }

    private void FixedUpdate()
    {
        if (_health.IsDeath == false)
        {
            Move();
            FindOutGround();

            if (_isJump)
                Jump();
        }     
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_movement * _speed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _isCanDoubleJump = true;
            _rigidbody.velocity = Vector3.up * _jumpForce;
            _isJump = false;
        }
        else if(_isCanDoubleJump)
        {
            _isCanDoubleJump = false;
            _rigidbody.velocity = Vector3.up * _jumpForce;
            _isJump = false;
            _readyToNextJump = false;
        }
    }

    private void FindOutGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -Vector2.up);

        float distance = Vector3.Distance(transform.position, hit2D.point);  

        if (hit2D == true && distance < maximumNearDistance)
        {
            _isGrounded = true;
            _readyToNextJump = true;
        }
        else
        {
            _isGrounded = false;
        }         
    }

    ////private void OnCollisionEnter2D(Collision2D collision)
    ////{


    ////    //Sequence _sequence = DOTween.Sequence();

    ////    //var initialColor = _spriteRenderer.color;

    ////    //if (collision.collider.TryGetComponent<Enemy>(out Enemy enemy))
    ////    //{
    ////    //    foreach (ContactPoint2D point in collision.contacts)
    ////    //    {
    ////    //        if (point.normal.y < 0 || point.normal.x > 0.1f || point.normal.x < -0.1f)
    ////    //        {
    ////    //            _health.TakeDamage();

    ////    //            _sequence.Append(DOTweenModuleSprite.DOColor(_spriteRenderer, Color.clear, 0.25f).SetLoops(2, LoopType.Yoyo));
    ////    //            _sequence.Append(_spriteRenderer.DOColor(initialColor, 1));

    ////    //            break;
    ////    //        }
    ////    //    }
    ////    //}
    ////}
}
