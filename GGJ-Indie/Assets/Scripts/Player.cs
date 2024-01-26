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

    public delegate void OnEnemyKillDelegate(Enemy enemy);
    public OnEnemyKillDelegate onEnemyKill; 
    
    // Start is called before the first frame update
    void Start()
    {
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
