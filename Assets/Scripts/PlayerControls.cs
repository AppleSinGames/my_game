using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D RgBody;
    public GameObject Player;
    public Transform Pos;
    public SpriteRenderer SpriteRenderer;
    public GameObject FireballPrefab;

    public float HorizontalInput     { get; set; }
    public float LockMove            { get; set; }
    public float LockFire            { get; set; }
    public float FireBallPosY        { get; set; } = 1f;
    public float CurrentFireBallX { get; set; } = 1f;
    public Vector2 CurrentFireBallPos { get; set; } 
    public static bool PlayerFlipped { get; set; }

    public int Health               { get; set; } = 1;
    public float FireCd             { get; set; } = 1.5f;
    public int Speed                { get; set; } = 6;
    public int JumpForce            { get; set; } = 10;
    public int AvailableJumps       { get; set; } = 2;
    public Vector2 SpawnPoint       { get; set; } = new(-39.2f, -15.84f);

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

    void OnJump(InputValue value)
    {
        if (value.isPressed && AvailableJumps > 0)
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
        Pos.position = SpawnPoint;
    }

    private void Update()
    {
        if (HorizontalInput < 0)
        {
            SpriteRenderer.flipX = true;
            PlayerFlipped = true;
        }

        if (HorizontalInput > 0)
        {
            SpriteRenderer.flipX = false;
            PlayerFlipped = false;
        }

        if (Health <= 0)
        {
            Destroy(Player);
            UiControl.Instance.DeathScreen();
        }

        if (LockMove > 0)
        {
            LockMove -= Time.deltaTime;
        }

        if (LockFire > 0)
        {
            LockFire -= Time.deltaTime;
        }

        CurrentFireBallPos = new(Pos.position.x, Pos.position.y + FireBallPosY);
    }
    private void FixedUpdate()
    {
        if (LockMove <= 0)
        {
            RgBody.linearVelocityX = HorizontalInput * Speed;
        }
    }
}
