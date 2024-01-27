using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    [SerializeField] float damage = 1.0f;
    [SerializeField] bool hasErraticMovement = false;
    [SerializeField] float erraticDistance = 7.0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null) 
        {
            player.ReduceHealth(damage);
        }

        Destroy(gameObject); //Enemigo se destruye
    }
}
