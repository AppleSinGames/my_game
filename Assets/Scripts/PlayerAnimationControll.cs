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
    private PlayerControls _controls;

    private void Animation()
    {

    }

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
            PlayerControls.IsJumping = true;
            PlayerControls.IsFalling = false;
        }
        else if (velocityY < -verticalVelocityThreshold)
        {
            PlayerControls.IsJumping = false;
            PlayerControls.IsFalling = true;
        }
        else
        {
            PlayerControls.IsJumping = false;
            PlayerControls.IsFalling = false;
        }

        bool isInAir = PlayerControls.IsJumping || PlayerControls.IsFalling;

        if (Mathf.Abs(velocityX) > horizontalVelocityThreshold && !isInAir)
        {
            PlayerControls.IsRunning = true;
        }
        else
        {
            PlayerControls.IsRunning = false;
        }

        _playerAnimator.SetBool(IsJumpingHash, PlayerControls.IsJumping);
        _playerAnimator.SetBool(IsFallingHash, PlayerControls.IsFalling);
        _playerAnimator.SetBool(IsRunningHash, PlayerControls.IsRunning);
    }
}
