using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private float _movement;
    private bool _isJump;
    private bool _isGrounded;

    //private RaycastHit2D hit2D;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
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
        Jump();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_movement * _speed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_isJump)
            _rigidbody.velocity = Vector3.up * _jumpForce;

        _isJump = false;
    }

    private void FindOutGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -Vector2.up);

        float distance = Vector3.Distance(transform.position, hit2D.point);

        if (hit2D == true && distance < 5 )
            _isGrounded = true; 
        else
            _isGrounded = false;
    }
}
