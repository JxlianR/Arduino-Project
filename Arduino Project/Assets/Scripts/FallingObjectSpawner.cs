using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;          
    public float spawnInterval = 1f;           
    public float spawnHeight = 10f;             
    public float minHorizontalSpeed = -2f;      
    public float maxHorizontalSpeed = 2f;       

    private Camera mainCamera;                  

    private void Start()
    {
        mainCamera = Camera.main;
        StartSpawning();
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        GameObject objectPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        float spawnX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);

        Vector3 spawnPosition = new Vector3(spawnX, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + spawnHeight, 0f);

        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float horizontalSpeed = Random.Range(minHorizontalSpeed, maxHorizontalSpeed);
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
        }

        DestroyOnGround destroyScript = spawnedObject.AddComponent<DestroyOnGround>();
        destroyScript.groundY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }
}
