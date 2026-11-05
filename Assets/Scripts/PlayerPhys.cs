using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerControls))]
public class PlayerPhys : MonoBehaviour
{
    private PlayerControls _controls;
    private bool _isDying = false;

    private IEnumerator RespawnRoutine()
    {
        _isDying = true;

        _controls.Health--;
        _controls.LockMove = 0.7f;
        _controls.HorizontalInput = 0;

        _controls.RgBody.linearVelocity = Vector2.zero;
        _controls.RgBody.angularVelocity = 0f;
        _controls.RgBody.SetRotation(0);

        _controls.RgBody.simulated = false;
        transform.position = _controls.SpawnPoint;

        yield return new WaitForFixedUpdate();

        _controls.RgBody.simulated = true;
        _isDying = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Fireball"))
        {
            _controls.LockMove = 0.5f;
            UnitsEnv.KnockBack(_controls.RgBody, collision);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            _controls.LockMove = 0.5f;
            UnitsEnv.GoDown(_controls.RgBody);
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            if (_isDying) return;

            StartCoroutine(RespawnRoutine());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerControls.IsJumping = false;

        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetContact(0).normal.y > 0.5f)
            {
                _controls.JumpAvailable = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerControls.IsJumping = true;

        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Enemy"))
        {
            _controls.JumpAvailable = false;
        }
    }

    private void Awake()
    {
        _controls = GetComponent<PlayerControls>();
    }
}
