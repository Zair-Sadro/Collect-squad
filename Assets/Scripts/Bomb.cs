using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject bombButton;
    [SerializeField] PlayerTowerGen generation;
    [SerializeField] GameObject bomba;


    public void Boom()
    {
        bomba.SetActive(true);
        bomba.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (generation.tower1 || generation.tower2 || generation.tower3)
            {
                bombButton.SetActive(true);
            }
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bombButton.SetActive(false);
        }
       
    }
}
