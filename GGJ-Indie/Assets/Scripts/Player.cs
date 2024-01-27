using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

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

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = faces[2];

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
        }
        else if (health > 0)
        {
            spriteRenderer.sprite = faces[health];
        }
    }

    private void GainHealth()
    {
        if (health < maxHealth)
        {
            health += 1;
            spriteRenderer.sprite = faces[health];
        }
    }

    public void EnemyKilled()
    {
        enemyKills += 1;
        if (enemyKills == enemiesToGetLife)
        {
            GainHealth();
            enemyKills = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyProjectile")
        {
            //collision.gameObject.damage     // Da�o del proyectil??
            Destroy(collision.gameObject);
            this.ReduceHealth(1);            // Da�o del proyectil??
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
