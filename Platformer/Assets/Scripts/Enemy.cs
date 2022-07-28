using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Transform))]
public class Enemy : MonoBehaviour, IInteractable
{   
    [SerializeField] private int _speed;

    private Transform _target;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private int _timeToDestroy = 2;
    private float _timeToBlinkRenderer = 0.25f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        var player = FindObjectOfType<PlayerController>();

        _target = player.transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerController>(out PlayerController player))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y < 0)
                {
                    _collider.enabled = false;
                    GameEvents.Current.TakeDamageFromPlayer();

                    DOTweenModuleSprite.DOBlendableColor(_spriteRenderer, Color.clear, _timeToBlinkRenderer).SetLoops(1, LoopType.Yoyo);

                    Destroy(gameObject, _timeToDestroy);
                }
            }        
        }
    }
   
    public void Interact()
    {
        GameEvents.Current.TakeDamageFromEnemy();
    }
}