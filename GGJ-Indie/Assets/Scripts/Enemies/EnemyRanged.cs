using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.U2D;
using static UnityEngine.GraphicsBuffer;

public class EnemyRanged : MonoBehaviour
{
    [SerializeField] float health = 2;
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float bulletSpeed = 3.0f;
    [SerializeField] int damage = 1;
    [SerializeField] float bulletDamage = 1.0f;
    [SerializeField] float rangeToShoot = 6.0f;
    [SerializeField] float shootCooldown = 1.0f;
    [SerializeField] GameObject projectile;

    private GameObject player;
    private bool bCanShoot;
    private Rigidbody2D rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        bCanShoot = true;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = (player.transform.position - transform.position).normalized;

        MoveToPlayer();
        RotateToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null) 
        {
            player.ReduceHealth(damage);
            Destroy(gameObject); //Enemigo se destruye
        }

        //if(collision.CompareTag("PlayerProjectile"))
        //{
        //    Destroy(collision.gameObject);              // Destruye proyectil
        //    if (health <= 0) { Destroy(gameObject); }   // Destruye enemigo
        //}
    }

    private void RotateToPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, direction);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, movementSpeed);

        rb.SetRotation(rotation);
    }

    private void MoveToPlayer()
    {
        if((player.transform.position - transform.position).magnitude <= rangeToShoot)
        {
            Shoot();
        }
        else
        {
            transform.position += movementSpeed * Time.deltaTime * direction;
        }
    }

    private void Shoot()
    {
        if(bCanShoot)
        {
            GameObject projectile_prefab = Instantiate(projectile, transform.position, transform.rotation);
            projectile_prefab.GetComponent<Rigidbody2D>().velocity = projectile_prefab.transform.up * bulletSpeed;
            StartCoroutine(ReloadShoot());
        }
    }

    IEnumerator ReloadShoot()
    {
        bCanShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        bCanShoot = true;
    }

    public void ReduceHealth(float amount)
    {
        health -= amount;
        if (health <= 0) 
        { 
            health = 0;

            player.GetComponent<Player>().EnemyKilled(3);
            Destroy(gameObject); 
        }
    }
}
