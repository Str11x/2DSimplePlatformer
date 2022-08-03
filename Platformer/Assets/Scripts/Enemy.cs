using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IInteractable
{   
    [SerializeField] private int _speed;

    private PlayerMovement _player;
    private Transform _target;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private int _timeToDestroy = 2;
    private float _timeToBlinkRenderer = 0.25f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y < 0)
                {
                    _collider.enabled = false;
                    _player.GiveDamage();

                    DOTweenModuleSprite.DOBlendableColor(_spriteRenderer, Color.clear, _timeToBlinkRenderer).SetLoops(1, LoopType.Yoyo);

                    Destroy(gameObject, _timeToDestroy);
                }
            }        
        }
    }

    public void Interact()
    {
        _player.TookDamage();
    }

    public void SetTargetToPursue(PlayerMovement player)
    {
        _target = player.transform;
        _player = player;
    }
}