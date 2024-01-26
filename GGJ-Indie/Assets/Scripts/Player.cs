using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health = 100;

    [SerializeField]
    private int enemyKills = 0;

    public int bulletSpreadCount = 0;
    public int piercingCount = 0;

    public delegate void OnEnemyKillDelegate(Enemy enemy);
    public OnEnemyKillDelegate onEnemyKill; 

    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = Vector3.up;
        Debug.Log("up" + v);
        Debug.DrawLine(transform.position, v * 100, Color.green, 10);
        const float a = 90f;
        const int n = 9;
        v = Quaternion.Euler(0, 0, -a / 2) * v;
        for(int i = 0; i < n; i++) {
            Color color = (i == 0 ? Color.blue : i == n-1 ? Color.magenta : Color.cyan);
            Vector3 vec = Quaternion.Euler(0, 0, a / 5 * i) * v;
            Debug.DrawLine(transform.position, vec * 100, color, 10);
        }
        Debug.Log("aa" + v);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReduceHealth(float amount)
    {
        health -= amount;
        if(health <= 0) { health = 0; }     
    }
}
