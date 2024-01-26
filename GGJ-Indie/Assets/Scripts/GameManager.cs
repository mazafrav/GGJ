using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private float progression = 0f;  

    [SerializeField]
    private float maxProgression = 100f;  



    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start() {
        if(!player) return;
        player.onEnemyKill += OnEnemyKill;
    }

    private void Destroy() {
        if(!player) return;
        player.onEnemyKill -= OnEnemyKill;
    }

    private void OnEnemyKill(Enemy enemy) {
        progression = Math.Max(progression + 1, maxProgression);
        Debug.Log("Progression: " + progression);
    }

    public Player GetPlayer() {
        return player;
    }
}
