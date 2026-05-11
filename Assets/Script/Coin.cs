using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float _points = 1;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (!colision.CompareTag("Player")) return;
        if(colision.gameObject.tag != "Player"){return;}
        CoinManager.Instance.AddAmount(_points);
        gameObject.SetActive(false);
    }
}