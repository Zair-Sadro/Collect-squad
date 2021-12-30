using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingBricks : MonoBehaviour
{
    public bool loseBricks = false;
    public bool loseBricksBridge = false;
    public float cooldown = 1;
    public float timer = 0;
    public int countBricks = 0;
    public bool lostBricks = false;
    public float losingSpeed = 0.5f;
    public int counter = 0;
    public int howMuch = 0;
    [SerializeField] int i = 0;
    PlayerTowerGen generation;
    public int vibrating = 0;


    private void Start()
    {
        if (PlayerPrefs.HasKey("Vibration"))
        {
            vibrating = PlayerPrefs.GetInt("Vibration");
        }
    }

    void Update()
    {  
        countBricks = GetComponent<BricksCounter2>().plates.Count;
        
        StartCoroutine(LoseBricks());

        if (lostBricks == true)
        {
            StopCoroutine(LoseBricks());
        }
    }
    
    IEnumerator LoseBricks()
    {
        if (loseBricks == true && lostBricks != true)
        {
            for (; i > counter - generation.isNeeded; i--)
            {
                Destroy(GetComponent<BricksCounter2>().plates[i - 1]);
                if (vibrating == 1)
                {
                    Vibration.Vibrate(25);
                }
                yield return new WaitForSeconds(losingSpeed);
                if (i <= 1)
                {
                    lostBricks = true;
                    loseBricks = false;
                    break;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        generation = other.GetComponent<PlayerTowerGen>();

        if (other.CompareTag("PlayerTower") && (GetComponent<BricksCounter2>().plates.Count >= 1) && generation.isBuild)
        {
            
            counter = GetComponent<BricksCounter2>().plates.Count;
            loseBricks = true;
            i = counter;
            howMuch = counter - generation.isNeeded;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        howMuch = 0;
        loseBricks = false;
    }
}
