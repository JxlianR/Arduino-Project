using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int minObjectsPerSpawn = 1;
    public int maxObjectsPerSpawn = 3;
    public float spawnInterval = 1f;
    public float spawnHeight = 10f;
    public float minVerticalSpeed = -5f;
    public float maxVerticalSpeed = -10f;

    public GameObject[] powerUpPrefabs;
    public int powerUpChance;

    private Camera mainCamera;

    [SerializeField]
    private GameObject warningUI;

    Player player;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        StartSpawning();
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnObjects", 0f, spawnInterval);
    }

    private void SpawnObjects()
    {
        // Chance of spawning a Power-Up
        if (Random.Range(0, 100) < powerUpChance && player.powerUpAvailable == false)
        {
            GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            StartCoroutine(SpawnObject(powerUpPrefab, true));
        }
        else
        {
            // Spawning obstacle objects
            int numObjectsToSpawn = Random.Range(minObjectsPerSpawn, maxObjectsPerSpawn + 1);

            for (int i = 0; i < numObjectsToSpawn; i++)
            {
                GameObject objectPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
                StartCoroutine(SpawnObject(objectPrefab, false));
            }
        }
    }

    private IEnumerator SpawnObject(GameObject prefab, bool isPowerUp)
    {
        float spawnX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
        Vector3 spawnPosition = new Vector3(spawnX, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + spawnHeight, 0f);

        //Spawn Warning -> Where the obstacle will fall down
        Vector3 spawnPos = new Vector3(spawnX, mainCamera.ViewportToWorldPoint(new Vector3(0, 0.93f, 0)).y, 0f);
        GameObject ui = Instantiate(warningUI, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float verticalSpeed = Random.Range(minVerticalSpeed, maxVerticalSpeed);
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
        }

        DestroyOnGround destroyScript = spawnedObject.AddComponent<DestroyOnGround>();
        destroyScript.groundY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        if (isPowerUp)
        {
            MakePickupable(spawnedObject);
        }

        Destroy(ui);
    }

    private void MakePickupable(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody2D>();
        }

        CircleCollider2D collider = obj.GetComponent<CircleCollider2D>();
        if (collider == null)
        {
            collider = obj.AddComponent<CircleCollider2D>();
        }

        // Add the PowerUpPickup script if not already attached
        PowerUpPickup pickupScript = obj.GetComponent<PowerUpPickup>();
        if (pickupScript == null)
        {
            pickupScript = obj.AddComponent<PowerUpPickup>();
        }
    }
}
