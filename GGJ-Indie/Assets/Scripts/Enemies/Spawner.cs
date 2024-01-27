using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject player;
    private bool bCanSpawn;

    [SerializeField] GameObject walker;
    [SerializeField] GameObject ranged;
    [SerializeField] GameObject anxiety;
    [SerializeField] float radius;
    [SerializeField] float spawnRate;
    [SerializeField] int killsNeededToIncreaseSpawn;
    [SerializeField] float spawnTimeReduce;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        direction = player.transform.position.normalized;
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
                int kills = player.GetComponent<Player>().GetEnemyKills();

                if (kills >= killsNeededToIncreaseSpawn)
                {
                    spawnRate -= spawnTimeReduce;
                }

                float randomX = Random.Range(-7.7f, 7.7f);//- (player.transform.position + direction).x;
                float randomY = Random.Range(-4.0f, 4.0f); //- (player.transform.position + direction).y;
                Vector3 posToSpawn = new Vector3(randomX, randomY, 0.0f);
                Instantiate(ranged, posToSpawn, Quaternion.identity);
                Debug.DrawLine(player.transform.position + direction, posToSpawn, Color.red, Time.deltaTime, false);

                StartCoroutine(ReloadSpawn());
            }

            
            /*if  (player.transform.position.X > PosX - BRadius && currentTarget.X < PosX + BRadius) &&
                (currentTarget.Y > PosY - BRadius && currentTarget.Y < PosY + BRadius)*/
        
        }
    }

    IEnumerator ReloadSpawn()
    {
        bCanSpawn = false;
        yield return new WaitForSeconds(spawnRate);
        bCanSpawn = true;
    }
}
