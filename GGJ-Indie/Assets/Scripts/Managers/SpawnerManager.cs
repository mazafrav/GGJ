using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float spawnRate = 5.0f;
    private Camera camera;

    float width = 0.0f;
    float height = 0.0f;

    void Start()
    {     
        camera = Camera.main;

        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }
 
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        width = camera.orthographicSize + 1;
        height = camera.orthographicSize * camera.aspect + 1;

        Instantiate(enemies[Random.Range(0,enemies.Length)], new Vector3(camera.transform.position.x + Random.Range(-width, width), camera.transform.position.y + Random.Range(-height, height)), Quaternion.identity);
    }
}
