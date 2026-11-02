using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D RgBody;
    [SerializeField] GameObject FireBall;
    [SerializeField] GameObject ExplosionPrefab;

    private float _speed = 10;


    private void Start()
    {

        if (PlayerControls.PlayerFlipped == false)
        {
            _speed = 10;
        }
        if (PlayerControls.PlayerFlipped == true)
        {
            _speed = -10;
        }

        RgBody.linearVelocity = new Vector2(_speed, RgBody.linearVelocity.y);
    }



    private void Explode()
    {
        GameObject Explosion = Instantiate(ExplosionPrefab, FireBall.transform.position, Quaternion.identity);
        Destroy(Explosion, 0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        Destroy(gameObject);
    }

}
