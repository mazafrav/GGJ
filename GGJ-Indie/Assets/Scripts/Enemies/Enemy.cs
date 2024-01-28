using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 2;
    [SerializeField] float speed = 3.0f;
    [SerializeField] int damage = 1;
    [SerializeField] bool hasErraticMovement = false;
    [SerializeField] float erraticDistance = 7.0f;
    [SerializeField] int enemyType = 0;
    private GameObject player;

    AudioSource audioSource;
    [SerializeField] AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        audioSource = GameObject.Find("AudioSource").transform.GetChild(3).gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if (hasErraticMovement) 
        {
            direction += new Vector3(Random.Range(-erraticDistance, erraticDistance), Random.Range(-erraticDistance, erraticDistance), 0.0f);
        }

        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Quitamos vida al jugador
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null) 
        {
            player.ReduceHealth(damage);
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject); //Enemigo se destruye
        }
    }

    public void ReduceHealth(float amount)
    {
        health -= amount;     
        if(health <= 0) 
        { 
            health = 0;
            player.GetComponent<Player>().EnemyKilled(enemyType);
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject); 
        }
    }
}
