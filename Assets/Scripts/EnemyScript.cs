using UnityEngine;
using System.Collections;

public class Enemy_script : MonoBehaviour
{

    //----------------------------UNITY----------------------------
    [SerializeField] Rigidbody2D _rgBody;
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    public Vector2 _selfSpawner;
    //----------------------------STATS----------------------------

    public Unit Enemy { get; } = new()
    {
        Speed = 5f,
        Direction = 1,
        KnockbackForce = 5f
    };
    private float _knockbackDur = 0.5f;

    //----------------------------STATES----------------------------
    private bool _stunned;
    private bool _isPatrolling;
    private bool _isGrounded;
    private float _groundCheckDist = 0.5f;
    private float _wallCheckDist = 0.5f;

    private IEnumerator KnockRoutine()
    {
        _stunned = true;
        yield return new WaitForSeconds(_knockbackDur);
        _stunned = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            StartCoroutine(KnockRoutine());
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Flip();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            UnitsEnv.KnockBack(collision.rigidbody, transform.position, Enemy.KnockbackForce);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
            PlayerUiControl.Score += 1;
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
        if (!_stunned && _isGrounded)
        {
            _rgBody.linearVelocityX = Enemy.Speed * Enemy.Direction;
        }
    }

    private void Flip()
    {
        Enemy.Direction *= -1;

        Vector3 scale = transform.localScale; 
        scale.x *= -1;                        
        transform.localScale = scale;
    }

    private void Start()
    {
        _selfSpawner = transform.position;
        _rgBody.linearVelocityX = 5f;
        _isPatrolling = true;
        Enemy.Direction = 1;
    }

    private void FixedUpdate()
    {
        bool _groundAhead = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDist, groundLayer);
        bool _wallAhead = Physics2D.Raycast(_wallCheck.position, Vector2.right * Enemy.Direction, _wallCheckDist, wallLayer);

        if ((_groundAhead == false || _wallAhead) && _isGrounded)
        {
            Flip();
        }

        if (_isPatrolling && !_stunned)
        {
            Patroll();
        }
    }
}


