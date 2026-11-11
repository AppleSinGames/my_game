using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D RgBody;
    [SerializeField] GameObject FireBallObj;
    [SerializeField] GameObject ExplosionPrefab;

    public void Launch(int _direction, float _speed)
    {
        RgBody.linearVelocity = new Vector2(_speed * _direction, 0);
    }
    private void Explode()
    {
        Instantiate(ExplosionPrefab, FireBallObj.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        Destroy(gameObject);
    }


}
