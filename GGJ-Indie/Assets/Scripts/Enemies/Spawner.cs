using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject player;
    private bool bCanSpawn;
    private GameObject gameManager;
    private float maxSpawnRate;
    private float playerProgress, previousProgress;
    // private List<int> enemies;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] GameObject normal;
    [SerializeField] GameObject depression;
    [SerializeField] GameObject ranged;
    [SerializeField] GameObject anxiety;
    [SerializeField] float radius;
    [SerializeField] float spawnRate;
    [SerializeField] float minSpawnRate;

    bool depressionadded = false;
    bool anxietyadded = false;
    bool rangedadded = false;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
        direction = player.transform.position.normalized;
        playerProgress = gameManager.GetComponent<GameManager>().GetProgression();
        previousProgress = gameManager.GetComponent<GameManager>().GetProgression();
        maxSpawnRate = spawnRate;
        enemies.Add(normal);
        bCanSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            direction = (player.transform.position).normalized * radius;
            Debug.DrawLine(player.transform.position, player.transform.position + direction, Color.green, Time.deltaTime, false);

            if (bCanSpawn)
            {

                if (playerProgress > 10.0f && !depressionadded)
                {
                    enemies.Add(depression);
                    depressionadded = true;
                }
                if (playerProgress > 25.0f && !anxietyadded)
                {
                    enemies.Add(anxiety);
                    anxietyadded = true;
                }
                if (playerProgress > 50.0f && !rangedadded)
                {
                    enemies.Add(ranged);
                    rangedadded = true;
                }




                GameObject enemy = ChooseEnemy();
                playerProgress = gameManager.GetComponent<GameManager>().GetProgression();

               if (playerProgress != previousProgress)
               {
                    if(maxSpawnRate - (playerProgress / 10) * (maxSpawnRate - minSpawnRate) >= 1.0f )
                        spawnRate = maxSpawnRate - (playerProgress / 10) * (maxSpawnRate - minSpawnRate);

                    previousProgress = playerProgress;
               }
         
                Vector3 v = player.transform.up;
                float angle = Random.Range(0, 360.0f);//- (player.transform.position + direction).x;

                v = Quaternion.Euler(0, 0, angle) * v;
                Vector3 spawnPos = player.transform.position + v * Random.Range(radius, 2 * radius);

                //Debug.DrawLine(player.transform.position, spawnPos, Color.red, 100, false);
                Instantiate(enemy, spawnPos, Quaternion.identity);
                StartCoroutine(ReloadSpawn());
            }
        }
    }

    IEnumerator ReloadSpawn()
    {
        bCanSpawn = false;
        yield return new WaitForSeconds(spawnRate);
        bCanSpawn = true;
    }

    private GameObject ChooseEnemy()
    {
        int index = Random.Range(0, enemies.Count);
        GameObject enemyToSpawn = enemies[index];
        return enemyToSpawn;
    }
}
