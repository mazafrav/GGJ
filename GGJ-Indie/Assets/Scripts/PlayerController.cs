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
    double a = 30f;

    [SerializeField]
    float speed;

    [SerializeField]
    private float speedBuffPercentage = 0.2f;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Sprite[] bulletSprites;

    Player player;

    Vector3 direction;

    GameManager gm;
    SpriteRenderer bulletSpriteRenderer;

    AudioSource audioClips;
    [SerializeField] AudioClip shootClip;
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        audioClips = GameObject.Find("AudioSource").transform.GetChild(3).gameObject.GetComponent<AudioSource>();
        Debug.Log(GameObject.Find("AudioSource").transform.GetChild(3).gameObject.name);
        bCanShoot = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gm.getCurrentBuff() == 2)
        {
            speed *= (1 + speedBuffPercentage);
        }
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
                float bulletDamage = 1;
                if (player.bulletSpreadCount == 1)
                {
                    bulletDamage = 1;
                } else if (player.bulletSpreadCount == 2)
                {
                    bulletDamage = 0.8f;
                } else if (player.bulletSpreadCount == 3)
                {
                    bulletDamage = 0.7f;
                }
                else if (player.bulletSpreadCount == 4)
                {
                    bulletDamage = 0.6f;
                }
                Vector3 v = player.transform.up;
                //num balas
                int n = player.bulletSpreadCount;

                if (bCanShoot)
                {
                    for (int i = 0; i < n; i++)
                    {                       
                        Vector3 vec = Quaternion.Euler(0, 0, (float)(-a / 2 + (a / (n - 1)) * i)) * v;                 
                        GameObject projectile_prefab = Instantiate(projectile, transform.position,Quaternion.identity);
                        projectile_prefab.GetComponent<Projectile>().damage = bulletDamage;
                        projectile_prefab.GetComponent<Rigidbody2D>().velocity = vec * bulletSpeed;      
                        bulletSpriteRenderer = projectile.GetComponent<SpriteRenderer>();
                        if (n == 2) bulletSpriteRenderer.sprite = bulletSprites[1];
                        else if (n == 3)bulletSpriteRenderer.sprite = bulletSprites[2];
                        else if (n == 4)bulletSpriteRenderer.sprite = bulletSprites[3];
                        else if (n == 5) bulletSpriteRenderer.sprite = bulletSprites[4];
                    }
                    audioClips.PlayOneShot(shootClip);
                    StartCoroutine(Reload());
                }
            }
            else if(bCanShoot) //only shoots one projectile
            {
                //spriteRenderer.sprite = bulletSprites[0];
                GameObject projectile_prefab = Instantiate(projectile, transform.position, transform.rotation);
                projectile_prefab.GetComponent<Rigidbody2D>().velocity = projectile_prefab.transform.up * bulletSpeed;
                bulletSpriteRenderer = projectile.GetComponent<SpriteRenderer>();
                bulletSpriteRenderer.sprite = bulletSprites[0];
                audioClips.PlayOneShot(shootClip);
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
