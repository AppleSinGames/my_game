using System.Collections;
using UnityEngine;
using static Unity.U2D.Physics.PhysicsShape;

[RequireComponent(typeof(PlayerControls))]
public class PlayerPhys : MonoBehaviour
{
    private float _knockbackDur = 0.5f;
    private PlayerControls _controls;
    private Unit _player;

    private IEnumerator RespawnRoutine()
    {
        _controls.State = PlayerControls.PlayerState.Dead;

        _player.Health--;
        PlayerUiControl.Instance.HealthUpdate();
        _controls.HorizontalInput = 0;

        _controls.RgBody.linearVelocity = Vector2.zero;
        _controls.RgBody.angularVelocity = 0f;
        _controls.RgBody.SetRotation(0);

        _controls.RgBody.simulated = false;
        transform.position = _controls.SpawnPoint.position;

        yield return new WaitForFixedUpdate();

        _controls.RgBody.simulated = true;
        _controls.State = PlayerControls.PlayerState.Normal;
    }

    private IEnumerator KnockRoutine()
    {
        _controls.State = PlayerControls.PlayerState.Knocked;
        yield return new WaitForSeconds(_knockbackDur);
        _controls.State = PlayerControls.PlayerState.Normal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.GetContact(0).normal.y > 0.5f)
            {
                _controls.AvailableJumps = 2;
            }
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
                _controls.AvailableJumps = 1;
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Fireball"))
        {
            StartCoroutine(KnockRoutine());
        }

        else if (collision.gameObject.CompareTag("Spike"))
        {
            if (_controls.State == PlayerControls.PlayerState.Dead)
            {
                return;
            }
            StartCoroutine(RespawnRoutine());
            PlayerUiControl.Instance.HealthUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            UiControl.Instance.WinScreen();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border") && !_controls.IsGrounded)
        {
            Vector2 normal = collision.GetContact(0).normal;
            if (normal.y > 0.5f || normal.y < -0.5f)
            {
                return;
            }

            if (_controls.State != PlayerControls.PlayerState.Dashing)
            {
                if (normal.x > 0.5f) 
                {
                    _player.Direction = 1;
                }
                else if (normal.x < -0.5f) 
                {
                    _player.Direction = -1;
                }
                _controls.RgBody.linearVelocity = Vector3.zero;

                _controls.State = PlayerControls.PlayerState.OnWall;
                _controls.RgBody.gravityScale = 0;
            }
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            _controls.IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _controls.AvailableJumps = 1;
            _controls.IsGrounded = false;
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            _controls.RgBody.gravityScale = 1;
            if (_controls.State == PlayerControls.PlayerState.OnWall)
            {
                _controls.State = PlayerControls.PlayerState.Normal;
            }
        }
    }

    private void Awake()
    {
        _controls = GetComponent<PlayerControls>();
        _player = _controls.Player;
    }
}
