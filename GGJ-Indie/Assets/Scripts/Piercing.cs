using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercing : MonoBehaviour
{
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
            Debug.Log("APLICANDO PIERCING");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.piercingCount++;
                Destroy(gameObject);
            }
        }
    }
}
