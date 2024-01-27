using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Quitamos vida al jugador
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.ReduceHealth(damage);
        }
    }
}
