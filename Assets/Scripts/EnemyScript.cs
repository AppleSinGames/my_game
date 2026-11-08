using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rgBody;
    [SerializeField] SpriteRenderer _sprite;

    private float _knockbackForce = 10f;
    private float _patrolDistance = 3f;
    private float _speed = 5f;
    private float _lockMove;
    private int _movingDirection = 1;
    private bool _isPatrolling;

    public Vector2 _selfSpawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            _lockMove = 0.7f;
            UnitsEnv.KnockBack(_rgBody, collision, _knockbackForce);
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            UnitsEnv.GoDown(_rgBody);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            _rgBody.linearVelocity = Vector2.zero;
            _isPatrolling = false;
        }
    }

    private void Patroll()
    {
        if (transform.position.x >= _selfSpawner.x + _patrolDistance)
        {
            _movingDirection = -1;
            _sprite.flipX = true;
        }
        else if (transform.position.x <= _selfSpawner.x - _patrolDistance)
        {
            _movingDirection = 1;
            _sprite.flipX = false;
        }

        if (_lockMove <= 0)
        {
            _rgBody.linearVelocityX = _speed * _movingDirection;
        }
    }

    private void Start()
    {
        _selfSpawner = transform.position;
        _rgBody.linearVelocityX = 5f;
        _isPatrolling = true;
    }

    private void FixedUpdate()
    {
        if (_isPatrolling)
        {
            Patroll();
        }
        Debug.Log(_isPatrolling);
    }
}


