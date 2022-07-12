using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _jumpForce = 15;
    [SerializeField] private float maximumNearDistance = 0.99f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private float _movement;
    private bool _isJump;
    private bool _isGrounded = true;
    private bool _isCanDoubleJump;
    private bool _readyToNextJump = true;

    //private RaycastHit2D hit2D;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _readyToNextJump)
           _isJump = true;

        if (_movement < 0)
            _spriteRenderer.flipX = true;
        else if (_movement > 0)
            _spriteRenderer.flipX = false;   
    }

    private void FixedUpdate()
    {
        Move();
        FindOutGround();

        if (_isJump)
            Jump();
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
}
