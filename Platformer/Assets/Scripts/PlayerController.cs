using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Interactor))]
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

    private float _blinkDuration = 0.25f;
    private float _blinkBackTimeDuration = 0.25f;
    private int _blinkLoopsAmount = 12;
    
    public Interactor Interactor { get; private set; }

    public Health Health => _health;

    public event Action TookDamageFromEnemy;
    public event Action DamageDone;

    private void Start()
    {
        TookDamageFromEnemy += BlinkFromDamage;

        Interactor = GetComponent<Interactor>();
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();      
    }

    private void OnDisable()
    {
        TookDamageFromEnemy -= BlinkFromDamage;
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
            _rigidbody.velocity = Vector2.up * _jumpForce;
            _isJump = false;
        }
        else if(_isCanDoubleJump)
        {
            _isCanDoubleJump = false;
            _rigidbody.velocity = Vector2.up * _jumpForce;
            _isJump = false;
            _readyToNextJump = false;
        }
    }

    private void FindOutGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -Vector2.up);

        float distance = Vector2.Distance(transform.position, hit2D.point);  

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

    private void BlinkFromDamage()
    {
        if(_health.IsDeath == false)
        {
            Sequence _sequence = DOTween.Sequence();
            var initialColor = _spriteRenderer.color;

            _sequence.Append(DOTweenModuleSprite.DOColor(_spriteRenderer, Color.clear, _blinkDuration).SetLoops(_blinkLoopsAmount, LoopType.Yoyo));
            _sequence.Append(_spriteRenderer.DOColor(initialColor, _blinkBackTimeDuration));
        }    
    }

    public void TookDamage()
    {
        TookDamageFromEnemy?.Invoke();
    }

    public void GiveDamage()
    {
        DamageDone?.Invoke();
    }
}