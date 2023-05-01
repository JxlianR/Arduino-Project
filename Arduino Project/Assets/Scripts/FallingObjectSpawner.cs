using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int minObjectsPerSpawn = 1;
    public int maxObjectsPerSpawn = 3;
    public float spawnInterval = 1f;
    public float spawnHeight = 10f;
    public float minVerticalSpeed = -5f;
    public float maxVerticalSpeed = -10f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartSpawning();
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnObjects", 0f, spawnInterval);
    }

    private void SpawnObjects()
    {
        int numObjectsToSpawn = Random.Range(minObjectsPerSpawn, maxObjectsPerSpawn + 1);

        for (int i = 0; i < numObjectsToSpawn; i++)
        {
            GameObject objectPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

            float spawnX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);

            Vector3 spawnPosition = new Vector3(spawnX, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + spawnHeight, 0f);

            GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float verticalSpeed = Random.Range(minVerticalSpeed, maxVerticalSpeed);
                rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            }

            DestroyOnGround destroyScript = spawnedObject.AddComponent<DestroyOnGround>();
            destroyScript.groundY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        }
    }
}
