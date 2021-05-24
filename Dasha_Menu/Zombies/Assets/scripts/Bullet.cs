using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float destroyTime;
    public LayerMask solid;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        zombiebehavior zombie = collision.GetComponent<zombiebehavior>();
        if ( zombie != null)
        {
            zombie.Damage(damage);
        }

        DestroyBullet();
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
