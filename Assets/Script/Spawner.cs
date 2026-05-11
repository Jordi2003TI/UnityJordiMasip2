using UnityEngine;

public class Spawner : MonoBehaviour
{
    float _elapsedTime = 0f;
    float _spawnTime = 0.2f;
    int _spawnCount = 0;
    int _quantityToSpawn = 20;
    public GameObject prefabToSpawn;


    // Update is called once per frame
    void Update()
    {
        if(_spawnCount >= _quantityToSpawn){return;}

        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= _spawnTime){
            _elapsedTime = 0f;
            _spawnCount++;
            GameObject spawedPrefab = Instantiate(prefabToSpawn);
            if(spawedPrefab.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)){
                rb.position = transform.position;
            }
        }
    }
}
