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
                GameObject enemy = ChooseEnemy();
                playerProgress = gameManager.GetComponent<GameManager>().GetProgression();

               if (playerProgress != previousProgress)
               {
                    if(maxSpawnRate - (playerProgress / 10) * (maxSpawnRate - minSpawnRate) >= 1.0f )
                        spawnRate = maxSpawnRate - (playerProgress / 10) * (maxSpawnRate - minSpawnRate);

                    previousProgress = playerProgress;
               }
                
                float randomX = Random.Range(-7.7f, 7.7f);//- (player.transform.position + direction).x;
                float randomY = Random.Range(-4.0f, 4.0f); //- (player.transform.position + direction).y;
                Vector3 posToSpawn = new Vector3(randomX * radius, randomY + radius, 0.0f);
                Instantiate(enemy, posToSpawn, Quaternion.identity);
               
                //Debug.DrawLine(player.transform.position + direction, posToSpawn, Color.red, Time.deltaTime, false);

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
        int index = 0;
        int rndMax = 0;
        int rndMin = 0;
        if (playerProgress > 20.0f)
        {
            rndMax = 1;
        }
        if (playerProgress > 40.0f)
        {
            rndMax = 2;
        }
        if (playerProgress > 60.0f)
        {
            rndMax = 3;
        }
        if (playerProgress > 80.0f)
        {
            rndMin = 1;
            rndMax = 3;
        }
        index = Random.Range(rndMin, rndMax);
        GameObject enemyToSpawn = enemies[index];
        return enemyToSpawn;
    }
}
