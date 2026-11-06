using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rgBody;
    private float _knockbackForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            UnitsEnv.KnockBack(_rgBody, collision, _knockbackForce);
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            UnitsEnv.GoDown(_rgBody);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }
}


