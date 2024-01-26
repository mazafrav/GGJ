using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int piercingCount = 0;
    Player player;
    bool hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        piercingCount = player.piercingCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.tag == "Player"))
        {
            if(player != null && piercingCount==0)
            {
                Destroy(gameObject);
            }
            else if (player != null && piercingCount > 0 && !hasHit)
            {
                player.piercingCount--;
                hasHit = true;
            }
        }
        
    }
}
