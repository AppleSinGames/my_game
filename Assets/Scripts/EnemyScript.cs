using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rgBody;
    private float _knockbackForce = 10f;
    private float _patrolDistance = 3f;
    private float _speed = 5f;
    private int _movingDirection = 1;
    public Vector2 _selfSpawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
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

    private void IsPatrolling()
    {
        if (transform.position.x >= _selfSpawner.x + _patrolDistance)
        {
            _movingDirection = -1;
        }
        else if (transform.position.x <= _selfSpawner.x + -_patrolDistance)
        {
            _movingDirection = 1;
        }

        _rgBody.linearVelocityX = _speed * _movingDirection;
    }

    private void Start()
    {
        _selfSpawner = transform.position;
        _rgBody.linearVelocityX = 5f;
    }

    private void FixedUpdate()
    {
        IsPatrolling();
        Debug.Log(_rgBody.linearVelocityX);
    }
}


