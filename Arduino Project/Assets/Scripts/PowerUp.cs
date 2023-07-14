using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration;
    public int newinitialSpawnInterval;

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

    public void ApplyEffect()
    {
        float oldValue = spawner.initialSpawnInterval;
        spawner.initialSpawnInterval = newinitialSpawnInterval;
        gameObject.SetActive(false);
        StartCoroutine(CancelEffect(oldValue));
    }

    IEnumerator CancelEffect(float oldValue)
    {
        yield return new WaitForSeconds(duration);
        spawner.initialSpawnInterval = oldValue;
        Destroy(gameObject);
    }
}
