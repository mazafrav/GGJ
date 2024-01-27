using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 1;
    int piercingCount = 0;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        piercingCount = player.piercingCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.tag == "Player"))
        {
            if(player != null && piercingCount==0)
            {
                //Quitamos vida a los enemigos
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ReduceHhealth(damage);
                }
                EnemyRanged enemyRanged = collision.GetComponent<EnemyRanged>();
                if(enemyRanged != null)
                {
                    enemyRanged.ReduceHhealth(damage);
                }

                Destroy(gameObject); //Se destruye el projectil
            }
            else if (player != null && piercingCount > 0)
            {
                piercingCount--;
            }
        }
    }
}
