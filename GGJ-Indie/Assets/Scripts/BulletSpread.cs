using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpread : MonoBehaviour
{
    [SerializeField] int maxBulletSpread = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.bulletSpreadCount < maxBulletSpread)
            {
                
                player.bulletSpreadCount++;
                Destroy(gameObject);
            }
        }
    }
}
