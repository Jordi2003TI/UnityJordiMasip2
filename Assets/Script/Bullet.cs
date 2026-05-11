using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir = Vector2.zero;
    public float speed = 5f;


    // Update is called once per frame
    void Update()
    {
        Vector2 dir2 = dir * speed * Time.deltaTime;
        Vector3 currentDir = new Vector3(dir.x, dir2.y,0);
        transform.position += currentDir;        
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            if(collision.gameObject.tag != "bullet")
            {
                Destroy(gameObject);
            }
        }
    }
}
