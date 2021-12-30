using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
   
    Vector3 offset;
   

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Broken") != null)
        {
            gameObject.GetComponent<MoveToBot>().enabled = true;
            gameObject.GetComponent<CameraMovement>().enabled = false;
        }

        else
        {
            transform.position = offset + player.position;
            
        }

        
    }
}
