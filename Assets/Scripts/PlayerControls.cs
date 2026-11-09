using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_2 = new (0.2f);
    public Transform SpawnPoint;
    public Rigidbody2D RgBody;
    public GameObject Player;
    public Transform Pos;
    public SpriteRenderer SpriteRenderer;
    public GameObject FireballPrefab;

    public float HorizontalInput { get; set; }
    public float LockMove { get; set; }
    public float LockFire { get; set; }
    public float FireBallPosY { get; set; } = 1f;
    public float CurrentFireBallX { get; set; } = 1f;
    public Vector2 CurrentFireBallPos { get; set; } 
    public static bool PlayerFlipped { get; set; }

    public int Health { get; set; } = 1;
    public float FireCd { get; set; } = 1.5f;
    public int Speed { get; set; } = 6;
    public float DashSpeed { get; set; } = 15;
    public float DashCd { get; set; } = 1;
    public float LockDash { get; set; } = 1;
    public int JumpForce { get; set; } = 8;
    public int AvailableJumps { get; set; } = 2;
    public int Direction { get; set; } = 1;

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
        if (value.isPressed)
        {
            LockMove = 20f;
            RgBody.gravityScale = 0;
            if (Direction != 0)
            {
                RgBody.linearVelocity = new Vector2(DashSpeed * Direction, 0);
            }
            StartCoroutine(nameof(DashRoutine));
        }
    }

    private IEnumerator DashRoutine()
    {
        yield return _waitForSeconds0_2;
        RgBody.linearVelocity = Vector2.zero;
        LockMove = 0;
        RgBody.gravityScale = 1;
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && AvailableJumps > 0 && LockMove <= 0)
        {
            RgBody.linearVelocityY = JumpForce;
            AvailableJumps -=1;
        }
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && LockFire <= 0)
        {
            Instantiate(FireballPrefab, CurrentFireBallPos, Quaternion.identity);
            LockFire = FireCd;

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayFireball();
            }
            PlayerUi.Instance.StartCD();
        }
    }

    private void Start()
    {
        PlayerFlipped = false;
        Pos.position = SpawnPoint.position;
    }

    private void Update()
    {
        if (HorizontalInput < 0)
        {
            SpriteRenderer.flipX = true;
            PlayerFlipped = true;
            Direction = -1;
        }
        else if (HorizontalInput > 0)
        {
            SpriteRenderer.flipX = false;
            PlayerFlipped = false;
            Direction = 1;
        }

        if (Health <= 0)
        {
            Destroy(Player);
            UiControl.Instance.DeathScreen();
        }
        if (LockFire > 0)
        {
            LockFire -= Time.deltaTime;
        }

        CurrentFireBallPos = new(Pos.position.x, Pos.position.y + FireBallPosY);
    }
    private void FixedUpdate()
    {
        if (LockMove > 0)
        {
            LockMove -= Time.fixedDeltaTime;
            return;
        }
        if (LockMove <= 0)
        {
            RgBody.linearVelocityX = HorizontalInput * Speed;
        }
    }
}
