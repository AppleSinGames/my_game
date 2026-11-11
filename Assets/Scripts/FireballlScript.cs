using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D RgBody;
    [SerializeField] GameObject FireBall;
    [SerializeField] GameObject ExplosionPrefab;

    private float _speed = 20;
    private float _direction;

    private void Start()
    {
        if (PlayerControls.PlayerFlipped == false)
        {
            _direction = 1;
        }
        if (PlayerControls.PlayerFlipped == true)
        {
            _direction = - 1;
        }

        RgBody.linearVelocity = new Vector2(_speed * _direction, RgBody.linearVelocity.y);
    }

    private void Explode()
    {
        Instantiate(ExplosionPrefab, FireBall.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        Destroy(gameObject);
    }
}
