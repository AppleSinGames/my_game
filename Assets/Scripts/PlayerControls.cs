using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D RgBody;
    public GameObject Player;
    public Transform Pos;
    public SpriteRenderer SpriteRenderer;
    public GameObject FireballPrefab;

    public float HorizontalInput    { get; set; }
    public float LockMove           { get; set; }
    public float LockFire           { get; set; }
    public float FireBallPos        { get; set; } = 1f;
    public float CurrentFireBallPos { get; set; } 
    public static bool PlayerFlipped { get; set; }
    
    public int Health               { get; set; } = 1;
    public float FireCd             { get; set; } = 1.5f;
    public int Speed                { get; set; } = 6;
    public int JumpForce            { get; set; } = 10;
    public bool JumpAvailable       { get; set; } = false;
    public Vector2 SpawnPoint       { get; set; } = new(-6.5f, -1.5f);

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
        if (value.isPressed && JumpAvailable)
        {
            RgBody.linearVelocityY = JumpForce;
        }
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && LockFire <= 0)
        {
            Instantiate(FireballPrefab, new Vector2(Pos.position.x + CurrentFireBallPos, Pos.position.y), Quaternion.identity);
            LockFire = FireCd;
            AudioManager.Instance.PlayFireball();
            PlayerUi.Instance.StartCD();
        }
    }

    private void Start()
    {
        PlayerFlipped = false;
        CurrentFireBallPos = FireBallPos;
    }

    private void Update()
    {
        if (HorizontalInput < 0)
        {
            SpriteRenderer.flipX = true;
            PlayerFlipped = true;
            CurrentFireBallPos = -1 * FireBallPos;
        }

        if (HorizontalInput > 0)
        {
            SpriteRenderer.flipX = false;
            PlayerFlipped = false;
            CurrentFireBallPos = FireBallPos;
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
    }
    private void FixedUpdate()
    {
        if (LockMove <= 0)
        {
            RgBody.linearVelocityX = HorizontalInput * Speed;

        }
    }
}
