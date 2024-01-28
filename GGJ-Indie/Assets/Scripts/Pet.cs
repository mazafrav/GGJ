using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField]
    private GameObject pet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null)
        {
           RotateAround newPet = Instantiate(pet, collision.transform).GetComponent<RotateAround>();
           if(newPet != null) 
           {
              newPet.SetRotateSpeed();
              newPet.SetClockwiseRotation();
           }
            GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>().PickPowerUp();
            Destroy(gameObject); //se destruye el power up
        }       
    }
}
