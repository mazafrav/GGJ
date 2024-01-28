using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    [SerializeField]
    private int maxHealth = 5;

    [SerializeField]
    private Sprite[] faces;

    [SerializeField]
    public int enemyKills = 0;

    [SerializeField]
    private int enemiesToGetLife = 25;
    private int enemiesToNextHealth = 0;

    public int bulletSpreadCount = 0;
    public int piercingCount = 0;

    public delegate void OnEnemyKillDelegate(Enemy enemy);
    public OnEnemyKillDelegate onEnemyKill;

    private SpriteRenderer spriteRenderer;

    private GameManager gameManager;
    private PowerUpManager pwMng;

    private int enemy1killed = 0;
    private int enemy2killed = 0;
    private int enemy3killed = 0;

    [SerializeField]
    private GameObject DVD;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.getCurrentBuff() == 1)
        {
            Instantiate(DVD, new Vector3(0,0,0), Quaternion.identity);
        }
        pwMng = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
        spriteRenderer.sprite = faces[health-1];

        Vector3 v = Vector3.up;
        //angulo
        const double a = 45f;
        //num balas
        const int n = 8;
        for (int i = 0; i < n; i++)
        {
            Color color = (i == 0 ? Color.blue : i == n - 1 ? Color.magenta : Color.cyan);
            //vec es el angulo de los disparos
            Vector3 vec = Quaternion.Euler(0, 0, (float) (-a / 2 + (a / (n - 1))  * i)) * v;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyKills >= 100) //Si matamos a todos los enemigos ganamos
        {
            gameManager.Win();
        }
    }

    public void ReduceHealth(int amount)
    {
        health -= amount;
        if (health <= 0) 
        {
            health = 0;
            int buff = 0;
            if (enemy3killed > enemy2killed)
            {
                buff = 3;
            }
            else if (enemy2killed > enemy1killed)
            {
                buff = 2;
            }
            else
            {
                buff = 1;
            }
            gameManager.setCurrentBuff(buff);
            Cursor.visible = true;
            gameManager.GameOver();
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
        enemyKills++;
        enemiesToNextHealth++;

        gameManager.IncreaseProgression();
        pwMng.hasKilled= true;

        if (enemiesToNextHealth == enemiesToGetLife) //ganr vida matando
        {
            GainHealth();
            enemiesToNextHealth = 0;
        }

        if (enemyType == 1) //depression
        {
            enemy1killed++;
        } else if (enemyType == 2) //anxiety
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
}
