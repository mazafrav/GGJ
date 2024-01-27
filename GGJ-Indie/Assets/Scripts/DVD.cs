using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVD : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    [SerializeField] private float speed = 7.5f;
    private Vector3 direction;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 randomDirection = new Vector3(Random.Range(0f, 5f), Random.Range(0f, 5f), 0f);
        rb.velocity = randomDirection.normalized * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entro en collision general");
        if (other.CompareTag("Vertical_Wall"))
        {
            //float randomX = Random.Range(0f, 5f);
            Vector3 randomDir = new Vector3(rb.velocity.x, rb.velocity.y * -1, 0);
            rb.velocity = randomDir;
            Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Debug.Log(randomColor.ToString());
            spriteRenderer.color = randomColor;
        }
        else if (other.CompareTag("Horizontal_Wall"))
        {
            //float randomY = Random.Range(0f, 5f);
            Vector3 randomDir = new Vector3(rb.velocity.x * -1, rb.velocity.y, 0);
            rb.velocity = randomDir;
            Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Debug.Log(randomColor.ToString());
            spriteRenderer.color = randomColor;
        }

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReduceHhealth(damage);
            }
            EnemyRanged enemyRanged = other.GetComponent<EnemyRanged>();
            if (enemyRanged != null)
            {
                enemyRanged.ReduceHhealth(damage);
            }
        }
    }
}
