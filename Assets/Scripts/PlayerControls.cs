using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_5 = new (0.5f);

    //----------------------------UNITY----------------------------
    public Transform SpawnPoint;
    public Rigidbody2D RgBody;
    public GameObject PlayerObj;
    public Transform Pos;
    public SpriteRenderer SpriteRenderer;
    public Vector2 CurrentFireBallPos { get; set; } 

    //----------------------------STATES----------------------------
    public float HorizontalInput { get; set; }
    public enum PlayerState {Dead, Knocked, Dashing, Normal, OnWall}
    public PlayerState State { get; set; } = PlayerState.Normal;
    public bool CanMove => State == PlayerState.Normal;
    public bool CanDash => State != PlayerState.Dead && Dash.IsReady;
    public bool CanJump => (State != PlayerState.Dead || State != PlayerState.Knocked) && AvailableJumps > 0;
    public bool CanShoot => State == PlayerState.Normal && Fireball.IsReady;
    public bool IsGrounded;

    //----------------------------STATS----------------------------
    public Unit Player { get; } = new()
    {
        Health = 3,
        MaxHealth = 3,
        Speed = 6,
        DashSpeed = 15,
        JumpForce = 8,
        Direction = 1
    };
    public int AvailableJumps { get; set; } = 2;
    public Vector2 WallJump = new(5, 5); 

    //----------------------------SPELLS----------------------------
    public Spell Dash { get; } = new()
    {
        Cooldown = 1,
        Duration = 0.25f
    };

    public Spell Fireball { get; } = new()
    {
        Cooldown = 1,
        Speed = 20,
    };

    public FireballScript FireballPrefab;
    private float _fireballOffsetY { get; } = 1f;


    void OnMove(InputValue value)
    {
        HorizontalInput = value.Get<Vector2>().x;
    }

    void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            UiControl.Instance.PauseGame();
        }
    }

    void OnDash(InputValue value)
    {
        if (value.isPressed && CanDash)
        {
            Dash.Use();

            StartCoroutine(DashRoutine());
            PlayerUiControl.Instance.Dash();
        }
    }

    private IEnumerator DashRoutine()
    {
        State = PlayerState.Dashing;
        RgBody.gravityScale = 0;
        if (Player.Direction != 0)
        {
            RgBody.linearVelocity = new Vector2(Player.DashSpeed * Player.Direction, 0);
        }

        yield return new WaitForSeconds(Dash.Duration);

        RgBody.linearVelocity = Vector2.zero;
        State = PlayerState.Normal;
        RgBody.gravityScale = 1;
    }

    private IEnumerator WallJumpRoutine()
    {
        State = PlayerState.Dashing;
        RgBody.linearVelocity = new (WallJump.x * Player.Direction, WallJump.y);

        yield return _waitForSeconds0_5;
        State = PlayerState.Normal;
        RgBody.linearVelocity = Vector2.zero;
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && CanJump)
        {
            if (State == PlayerState.OnWall)
            {
                StartCoroutine(WallJumpRoutine());
            }
            else
            {
                RgBody.linearVelocityY = Player.JumpForce;
                AvailableJumps -=1;
            }
        }
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && CanShoot)
        {
            Fireball.Use();

            var position = new Vector2(Pos.position.x, Pos.position.y + _fireballOffsetY);
            var fireball = Instantiate(FireballPrefab, position, Quaternion.identity);
            fireball.Launch(Player.Direction, Fireball.Speed);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayFireball();
            }
            PlayerUiControl.Instance.Fireball();
        }
    }

    private void Start()
    {
        Pos.position = SpawnPoint.position;
        Player.Health = Player.MaxHealth;
    }

    private void Update()
    {
        if (HorizontalInput < 0)
        {
            Player.Direction = -1;
        }
        else if (HorizontalInput > 0)
        {
            Player.Direction = 1;
        }
        if (Player.Direction == 1)
        {
            SpriteRenderer.flipX = false;
        }
        else if (Player.Direction == -1)
        {
            SpriteRenderer.flipX = true;
        }

        if (Player.Health <= 0)
        {
            Destroy(PlayerObj);
            UiControl.Instance.DeathScreen();
        }
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            RgBody.linearVelocityX = HorizontalInput * Player.Speed;
        }
    }
}
