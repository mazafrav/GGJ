using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    [SerializeField]
    private int maxHealth = 5;

    [SerializeField]
    private Sprite[] faces;

    [SerializeField]
    private int enemyKills = 0;

    [SerializeField]
    private int enemiesToGetLife = 25;

    public int bulletSpreadCount = 0;
    public int piercingCount = 0;

    public delegate void OnEnemyKillDelegate(Enemy enemy);
    public OnEnemyKillDelegate onEnemyKill;

    private SpriteRenderer spriteRenderer;

    private int enemy1killed = 0;
    private int enemy2killed = 0;
    private int enemy3killed = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = faces[health-1];

        Vector3 v = Vector3.up;
        Debug.Log("up" + v);
        //angulo
        const double a = 45f;
        //num balas
        const int n = 8;
        Debug.DrawLine(transform.position, v * 100, Color.green, 100);
        for (int i = 0; i < n; i++)
        {
            Color color = (i == 0 ? Color.blue : i == n - 1 ? Color.magenta : Color.cyan);
            //vec es el angulo de los disparos
            Vector3 vec = Quaternion.Euler(0, 0, (float) (-a / 2 + (a / (n - 1))  * i)) * v;
            Debug.DrawLine(transform.position, vec * 100, Color.white, 100);
            Debug.Log(v);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReduceHealth(int amount)
    {
        health -= amount;
        if (health <= 0) 
        {
            health = 0;
            SceneManager.LoadScene(3);
        }
        UpdateSprite();
    }

    private void GainHealth()
    {
        if (health < maxHealth)
        {
            health += 1;
        }
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (health > 0)
        {
            spriteRenderer.sprite = faces[health - 1];
        }
        
    }

    public void EnemyKilled(int enemyType)
    {
        enemyKills += 1;
        if (enemyKills == enemiesToGetLife)
        {
            GainHealth();
            enemyKills = 0;
        }
        if (enemyType == 1) //anxiety
        {
            enemy1killed++;
        } else if (enemyType == 2) //depression
        {
            enemy2killed++;
        } else if (enemyType == 3) //elotro
        {
            enemy3killed++;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyProjectile")
        {
            //collision.gameObject.damage     // Daño del proyectil??
            Destroy(collision.gameObject);
            this.ReduceHealth(1);            // Daño del proyectil??
        }
    }
    public int GetEnemyKills()
    {
        return enemyKills;
    }

    public void IncreaseKills()
    {
        enemyKills += 1;
    }
}
