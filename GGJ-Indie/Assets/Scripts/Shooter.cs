using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float speed;

    GameObject playerRef;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
            GameObject projectile_prefab = Instantiate(projectile, spawnPoint, transform.rotation);
            projectile_prefab.GetComponent<Rigidbody2D>().velocity = projectile_prefab.transform.up * speed;
        }
    }
}
