using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGenerate : MonoBehaviour
{
    [SerializeField] BotLosingBricks losingBricks;
    public int count = 0;
    [SerializeField] GameObject sold1;
    [SerializeField] GameObject sold2;
    [SerializeField] GameObject sold3;
    [SerializeField] ParticleSystem upgrade;
    [SerializeField] BotBrickCounter brickConter2;
    [SerializeField] BotMovement movement;
    GameObject soldy;
    public int countSoldiers = 7;
    public float delay = 3;
    float rand;
    public float radius = 2;
    public int countStatic = 0;
    public bool sold1Done = false;
    public bool sold2Done = false;
    public List<GameObject> solds = new List<GameObject>();
    int k = 0;
    int l = 0;
    int m = 0;
    public int n = 0;
    private void Start()
    {
        upgrade.Stop();
        solds.Capacity = countSoldiers * 3;
        //n = countSoldiers - 1;
    }

    void Update()
    {

        rand = Random.Range(-radius, radius + 1);
        if (losingBricks.lostBricks == true)
        {

            StartCoroutine("Sold1Type");
        }

        if (count == 0 && losingBricks.lostBricks == true)
        {
            losingBricks.lostBricks = false;
            losingBricks.counter = 0;
            brickConter2.heightOffset = 0;
            for (int i = 0; i < brickConter2.plates.Count; i++)
            {
                brickConter2.plates.RemoveAt(i);
            }
        }
    }

    IEnumerator Sold1Type()
    {
        for (int i = 0; i < count; i++)
        {
            if (countStatic <= countSoldiers)
            {
                if (sold1Done == false && count <= countSoldiers)
                {

                    soldy = Instantiate(sold1, transform);
                    soldy.transform.position = new Vector3(rand, transform.position.y + 0.5f, transform.position.z + 3);
                    count--;
                    solds.Insert(0, soldy);
                    m++;
                    print("sold1");
                    
                }

            }
            
                if (countStatic > countSoldiers && countStatic <= countSoldiers * 2)
                {
                for (; k < countSoldiers; k++)
                {
                    for (; m <= countSoldiers - 1; m++)
                    {
                        if (sold1Done == false && solds[m] == null)
                        {
                            soldy = Instantiate(sold1, transform);
                            soldy.transform.position = new Vector3(rand, transform.position.y + 0.5f, transform.position.z + 3);
                            solds.Insert(0, soldy);
                        }
                    }
                    
                }

                for (int j = 0; j < count; j++)
                {
                    if (sold1Done == true && sold2Done == false/* && solds[n] == null*/)
                    {
                        soldy = Instantiate(sold2, transform);
                        soldy.transform.position = new Vector3(rand, transform.position.y + 0.5f, transform.position.z + 3);
                        count--;
                        solds.Insert(0, soldy);
                        n++;
                        print("sold2");
                        print(n);
                    }
                }
                   
                }

            if (countStatic > countSoldiers * 2)
            {
                for (; k < count; k++)
                {
                    for (; m <= countSoldiers - 1; m++)
                    {
                        if (sold1Done == false && solds[m] == null)
                        {
                            soldy = Instantiate(sold1, transform);
                            soldy.transform.position = new Vector3(rand, transform.position.y + 0.5f, transform.position.z + 3);
                            solds.Insert(0, soldy);
                            print(m);
                        }
                    }
                }

                yield return new WaitForSeconds(3);

                for (; l < countSoldiers; l++)
                {
                    for (; n < countSoldiers * 2; n++)
                    {
                        if (sold2Done == false && solds[n + countSoldiers] == null)
                        {
                            soldy = Instantiate(sold2, transform);
                            soldy.transform.position = new Vector3(rand, transform.position.y + 0.5f, transform.position.z + 3);
                            solds.Insert(0, soldy);
                            print(l);
                        }
                    }

                }

                for (int j = 0; j < count; j++)
                {
                    if (sold2Done == true)
                    {
                        soldy = Instantiate(sold3, transform);
                        soldy.transform.position = new Vector3(rand, transform.position.y + 0.5f, transform.position.z + 3);
                        count--;
                        solds.Add(soldy);
                        print("sold3");
                    }
                }

            }

                if (countStatic <= countSoldiers)
                {
                    sold1Done = false;
                }

                if (countStatic <= countSoldiers * 2)
                {
                    sold2Done = false;
                }

                yield return new WaitForSeconds(delay);

            if (countStatic >= countSoldiers)
            {
                upgrade.Play();
                sold1Done = true;
            }

            if (countStatic >= countSoldiers * 2)
            {
                upgrade.Play();
                sold2Done = true;
            }

            if (countStatic >= countSoldiers * 3)
            {
                upgrade.Play();
            }

        }
        yield return new WaitForSeconds(delay);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            count = brickConter2.bricksCount;
            countStatic += count;
            movement.waiting = true;
            print("enter");
            if (countStatic > countSoldiers && countStatic <= countSoldiers * 2)
            {
                count = countStatic - countSoldiers;
            }

            if (countStatic > countSoldiers * 2)
            {
                count = countStatic - countSoldiers * 2;
            }
        }
    }

}
