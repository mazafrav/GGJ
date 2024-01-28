using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float damage = 1;

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
        if (!player) return;
        if(collision.gameObject.layer.Equals("Wall")) Destroy(gameObject);
        if (collision.tag.Equals("Player") ||
            collision.tag.Equals("PlayerProjectile") ||
            collision.tag.Equals("Pet") ||
            collision.tag.Equals("PowerUp")) return;

        //Quitamos vida a los enemigos
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ReduceHealth(damage);
        }
        else
        {
            EnemyRanged enemyRanged = collision.GetComponent<EnemyRanged>();
            if (enemyRanged != null)
            {
                enemyRanged.ReduceHealth(damage);
            }
        }
        Debug.Log(piercingCount + collision.gameObject.name + " - " + collision.gameObject.tag);
        if (piercingCount <= 0) Destroy(gameObject); //Se destruye el projectil
        else piercingCount--;
    }
}
