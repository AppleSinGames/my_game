using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Rigidbody2D _rgBody;

    [SerializeField] Vector2[] Positions;
    private float _lockMove;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            _lockMove = 0.5f;
            UnitsEnv.KnockBack(_rgBody, collision);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            _lockMove = 0.5f;
            UnitsEnv.GoDown(_rgBody);
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            UiControl.Score++;
            Instantiate(_enemyPrefab, Positions[Random.Range(0, Positions.Length)], Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _lockMove -= Time.deltaTime;
    }
}
