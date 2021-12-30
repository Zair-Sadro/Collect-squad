using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLosingBricks : MonoBehaviour
{
    public bool loseBricks = false;
    public float cooldown = 1;
    public float timer = 0;
    public int countBricks = 0;
    public bool lostBricks = false;
    public float losingSpeed = 0.5f;
    public int counter = 0;
    [SerializeField] TowerGeneration generation;
    int i = 0;


    void Update()
    {
        countBricks = GetComponent<BotBrickCounter>().plates.Count;
        i = countBricks;
        StartCoroutine(LoseBricks());

        if (generation.count == 0 && lostBricks == true)
        {
            StopCoroutine(LoseBricks());
        }
    }

    IEnumerator LoseBricks()
    {
        if (loseBricks == true && lostBricks != true)
        {

            for (; i > 0; i--)
            {

                Destroy(GetComponent<BotBrickCounter>().plates[i - 1]);
                yield return new WaitForSeconds(losingSpeed);

                if (i <= 1)
                {
                    lostBricks = true;
                    loseBricks = false;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BotTower") && (GetComponent<BotBrickCounter>().plates.Count >= 1))
        {
            counter = GetComponent<BotBrickCounter>().plates.Count;
            loseBricks = true;
            
        }
    }
}
