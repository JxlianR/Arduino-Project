using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration;
    public int newSpawnInterval;

    FallingObjectSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<FallingObjectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyEffect()
    {
        float oldValue = spawner.spawnInterval;
        spawner.spawnInterval = newSpawnInterval;
        StartCoroutine(CancelEffect(oldValue));
    }

    IEnumerator CancelEffect(float oldValue)
    {
        yield return new WaitForSeconds(duration);
        spawner.spawnInterval = oldValue;
    }
}
