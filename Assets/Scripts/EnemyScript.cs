using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rgBody;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    private float _groundCheckDist = 0.5f;
    private float _wallCheckDist = 0.5f;
    private float _knockbackForce = 20f;
    private float _speed = 5f;
    private int _direction = 1;
    private float _lockMove;
    private bool _isPatrolling;
    private bool _isGrounded;

    public Vector2 _selfSpawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            _lockMove = 0.4f;
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            UnitsEnv.GoDown(_rgBody);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            UnitsEnv.KnockBack(collision.rigidbody, transform.position  , _knockbackForce);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGrounded = false;
        }
    }


    private void Patroll()
    {
        if (_lockMove <= 0)
        {
            _rgBody.linearVelocityX = _speed * _direction;
        }
    }

    private void Flip()
    {
        _direction *= -1;

        Vector3 scale = transform.localScale; 
        scale.x *= -1;                        
        transform.localScale = scale;
    }

    private void Start()
    {
        _selfSpawner = transform.position;
        _rgBody.linearVelocityX = 5f;
        _isPatrolling = true;
        _direction = 1;
    }

    private void FixedUpdate()
    {
        bool _groundAhead = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDist, groundLayer);
        bool _wallAhead = Physics2D.Raycast(_wallCheck.position, Vector2.right * _direction, _wallCheckDist, wallLayer);

        if ((_groundAhead == false || _wallAhead) && _isGrounded)
        {
            Flip();
        }

        if (_isPatrolling && _lockMove <= 0)
        {
            Patroll();
        }

        _lockMove -= Time.fixedDeltaTime;
    }
}


