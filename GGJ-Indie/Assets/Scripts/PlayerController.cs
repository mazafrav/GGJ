using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float velX, velY;
    private Rigidbody2D rb;
    private bool bCanShoot;

    [SerializeField]
    float fireRate;

    [SerializeField]
    float speed;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    GameObject projectile;

    Player player;

    Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        bCanShoot = true;
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
        if (Input.GetButton("Fire1"))
        {
            if(player.bulletSpreadCount > 1) 
            {
                Vector3 v = player.transform.up;
                //angulo
                const double a = 60f;
                //num balas
                int n = player.bulletSpreadCount;
              
                if (bCanShoot)
                {
                    for (int i = 0; i < n; i++)
                    {                       
                        Vector3 vec = Quaternion.Euler(0, 0, (float)(-a / 2 + (a / (n - 1)) * i)) * v;                 
                        GameObject projectile_prefab = Instantiate(projectile, transform.position,Quaternion.identity);
                        projectile_prefab.GetComponent<Rigidbody2D>().velocity = vec * bulletSpeed;                 
                    }

                    StartCoroutine(Reload());
                }
            }
            else if(bCanShoot) //only shoots one projectile
            {
                
                GameObject projectile_prefab = Instantiate(projectile, transform.position, transform.rotation);
                projectile_prefab.GetComponent<Rigidbody2D>().velocity = projectile_prefab.transform.up * bulletSpeed;
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {
        bCanShoot = false;
        yield return new WaitForSeconds(fireRate);
        bCanShoot = true;
    }
}
