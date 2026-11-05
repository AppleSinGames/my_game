using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerAnimationControll : MonoBehaviour
{
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
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
            _playerAnimator.SetBool(IsJumpingHash, PlayerControls.IsJumping);
            _playerAnimator.SetBool(IsRunningHash, false);
    }
}
