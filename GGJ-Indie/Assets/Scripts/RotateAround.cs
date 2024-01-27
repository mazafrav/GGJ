using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private float Xoffset = 1.0f;
    [SerializeField] private float Yoffset = 1.0f;
    [SerializeField] private int damage = 1;
    private GameObject centerPoint;

    [SerializeField] private float rotSpeed = 1.0f;
    [SerializeField] private bool rotateClockWise = true;

    float timer = 0;

    private void Start()
    {

        centerPoint = GameObject.Find("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime * rotSpeed;
        Rotate();
    }

    void Rotate()
    {
        if (rotateClockWise)
        {
            float x = -Mathf.Cos(timer) * Xoffset;
            float z = Mathf.Sin(timer) * Yoffset;
            Vector3 pos = new Vector3(x, z, 0f);
            transform.position = pos + centerPoint.transform.position;
        }
        else
        {
            float x = Mathf.Cos(timer) * Xoffset;
            float z = Mathf.Sin(timer) * Yoffset;
            Vector3 pos = new Vector3(x, z, 0f);
            transform.position = pos + centerPoint.transform.position;
        }
    }

    public void SetRotateSpeed()
    {
        rotSpeed = Random.Range(1.0f, 2.0f);
    }

    public void SetClockwiseRotation()
    {
        rotateClockWise = Random.Range(0, 2) == 0 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null) {
                enemy.ReduceHealth(damage);
                return;
            }

            EnemyRanged eRanged = other.gameObject.GetComponent<EnemyRanged>();
            if(eRanged != null) {
                eRanged.ReduceHealth(damage);
            }
        }
         
    }
}
