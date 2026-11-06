using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerAnimationControll : MonoBehaviour
{
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    private static readonly int IsFallingHash = Animator.StringToHash("IsFalling");
    [SerializeField] private float verticalVelocityThreshold = 0.3f;
    [SerializeField] private float horizontalVelocityThreshold = 0.1f;
    [SerializeField] Animator _playerAnimator;
    public static bool IsJumping = false;
    public static bool IsFalling = false;
    public static bool IsRunning = false;
    bool IsInAir = false;
    private PlayerControls _controls;

    private void Awake()
    {
        _controls = GetComponent<PlayerControls>();
    }

   private void Update()
    {
        float velocityY = _controls.RgBody.linearVelocityY;
        float velocityX = _controls.RgBody.linearVelocityX;

        if (velocityY > verticalVelocityThreshold)
        {
            IsJumping = true;
            IsFalling = false;
        }
        else if (velocityY < -verticalVelocityThreshold)
        {
            IsJumping = false;
            IsFalling = true;
        }
        else
        {
            IsJumping = false;
            IsFalling = false;
        }

        IsInAir = IsJumping || IsFalling;

        if (Mathf.Abs(velocityX) > horizontalVelocityThreshold && !IsInAir)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }

        _playerAnimator.SetBool(IsJumpingHash, IsJumping);
        _playerAnimator.SetBool(IsFallingHash, IsFalling);
        _playerAnimator.SetBool(IsRunningHash, IsRunning);
    }
}
