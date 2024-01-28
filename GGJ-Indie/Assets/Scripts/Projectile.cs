using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 1;

    int piercingCount = 0;
    Player player;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        piercingCount = player.piercingCount;

        if (gm.getCurrentBuff() == 3)
        {
            damage = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.tag == "Player") && collision.tag != "PlayerProjectile")
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

                if(collision.tag != "Pet")Destroy(gameObject); //Se destruye el projectil
            }
            else if (player != null && piercingCount > 0)
            {
                piercingCount--;
            }
        }
    }
}
