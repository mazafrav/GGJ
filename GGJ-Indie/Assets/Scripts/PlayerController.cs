using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float velX, velY;
    Rigidbody2D rb;

    [SerializeField]
    float speed;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    GameObject projectile;

    Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(velX * speed, velY * speed);
    }

    private void Aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile_prefab = Instantiate(projectile, transform.position, transform.rotation);
            projectile_prefab.GetComponent<Rigidbody2D>().velocity = projectile_prefab.transform.up * bulletSpeed;
        }
    }
}
