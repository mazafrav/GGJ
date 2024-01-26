using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float velX, velY;
    Rigidbody2D rb;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velX = Input.GetAxisRaw("Horizontal"); 
        velY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(velX * speed, velY* speed);             
    }

    private void FixedUpdate()
    {
    }
}
