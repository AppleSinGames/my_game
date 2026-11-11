using System.Collections;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] PlayerControls _controls;

    public Spell Explosion { get; } = new()
    {
        Duration = 0.2f,
        KnockbackForce = 10f
    };

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(Explosion.Duration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            return;
        }
        else
        {
            UnitsEnv.KnockBack(collision.rigidbody, transform.position, Explosion.KnockbackForce);
        }
    }

    private void Start()
    {
        StartCoroutine(DestroyRoutine());
    }
}
