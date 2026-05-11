using UnityEngine;

public class CheckGrounded : MonoBehaviour
{
    public bool isGrounded=false;
    [SerializeField] LayerMask _MaskIsGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Tocando: " + collision.gameObject.name + " Layer: " + collision.gameObject.layer);
        if((_MaskIsGround & (1 << collision.gameObject.layer)) != 0){
            isGrounded = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision){
        Debug.Log("Tocando: " + collision.gameObject.name + " Layer: " + collision.gameObject.layer);
        if((_MaskIsGround & (1 << collision.gameObject.layer)) != 0){
            isGrounded = false;
        }
        
    }
}
