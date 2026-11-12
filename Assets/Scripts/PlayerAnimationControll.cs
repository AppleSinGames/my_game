using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerAnimationControll : MonoBehaviour
{
    //----------------------------HASH----------------------------
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    private static readonly int IsFallingHash = Animator.StringToHash("IsFalling");

    //----------------------------UNITY----------------------------
    [SerializeField] private float verticalVelocityThreshold = 0.3f;
    [SerializeField] private float horizontalVelocityThreshold = 0.1f;
    [SerializeField] Animator _playerAnimator;

    //----------------------------STATES----------------------------
    public static bool _isJumping = false;
    public static bool _isFalling = false;
    public static bool _isRunning = false;
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
            _isJumping = true;
            _isFalling = false;
        }
        else if (velocityY < -verticalVelocityThreshold)
        {
            _isJumping = false;
            _isFalling = true;
        }
        else
        {
            _isJumping = false;
            _isFalling = false;
        }

        IsInAir = _isJumping || _isFalling;

        if (Mathf.Abs(velocityX) > horizontalVelocityThreshold && !IsInAir)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }

        _playerAnimator.SetBool(IsJumpingHash, _isJumping);
        _playerAnimator.SetBool(IsFallingHash, _isFalling);
        _playerAnimator.SetBool(IsRunningHash, _isRunning);
    }
}
