using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir = Vector2.zero;
    public float speed = 5f;

    void Update()
    {
        Vector3 movement = new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;
        transform.position += movement;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // FIX: TakeDamage primero, luego destruir bala
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage();

            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag != "bullet")
            {
                Destroy(gameObject);
            }
        }
    }
}