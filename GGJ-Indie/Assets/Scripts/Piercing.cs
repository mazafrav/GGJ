using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercing : MonoBehaviour
{
    [SerializeField]
    private int max_piercing_hits = 5;
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
            if (player != null)
            {
                if (player.piercingCount < max_piercing_hits)
                {
                    player.piercingCount++;
                }
                GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>().PickPowerUp();
                Destroy(gameObject);
            }
        }
    }
}
