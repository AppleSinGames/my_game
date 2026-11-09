using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _konockbackForce = 10f;
    private static WaitForSeconds _waitForSeconds0_5 = new (0.5f);

    private IEnumerator DestroyRoutine()
    {
        yield return _waitForSeconds0_5;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            return;
        }
        else
        {
            UnitsEnv.KnockBack(collision.rigidbody, transform.position, _konockbackForce);
        }
    }

    private void Start()
    {
        StartCoroutine(nameof(DestroyRoutine));
    }
}
