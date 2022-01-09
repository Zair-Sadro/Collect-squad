using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGeneration : MonoBehaviour
{
    [SerializeField] BotLosingBricks losingBricks;
    public int count = 0;
    public GameObject[] towers = new GameObject[3];
    [SerializeField] ParticleSystem upgrade;
    [SerializeField] BotBrickCounter brickConter2;
    [SerializeField] BotMovement movement;
    [SerializeField] GameObject bowLvl1;
    [SerializeField] GameObject spearLvl1;
    [SerializeField] GameObject swordLvl1;
    [SerializeField] GameObject bowLvl2;
    [SerializeField] GameObject spearLvl2;
    [SerializeField] GameObject swordLvl2;
    [SerializeField] GameObject bowLvl3;
    [SerializeField] GameObject spearLvl3;
    [SerializeField] GameObject swordLvl3;
    [SerializeField] GameObject zabor;
    float rand;
    public int countBricks = 10;
    public float delay = 3;
    public int countStatic = 0;
    public int randInt = 0;
    public bool isBuild = false;
    public bool tower1 = false;
    public bool tower2 = false;
    public bool tower3 = false;
    public float hp = 10;
    public bool broken = false;

    private void Start()
    {
        countStatic = -countBricks;
        rand = Random.Range(0, 3);
        randInt = System.Convert.ToInt32(rand);
    }

    void Update()
    {

        if (isBuild)
        {
            StartCoroutine("Sold1Type");
            count = 0;
        }

        if (!isBuild)
        {
            StopCoroutine("Sold1Type");
        }

        if (losingBricks.lostBricks == true && count == 0)
        {
            losingBricks.lostBricks = false;
            losingBricks.counter = 0;
            brickConter2.heightOffset = brickConter2.saveHeight;
            brickConter2.offset = brickConter2.saveOffset;
            for (int i = 0; i < brickConter2.plates.Count; i++)
            {
                brickConter2.plates.RemoveAt(i);
            }
            isBuild = false;
            
        }

        if (hp <= 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            bowLvl1.transform.parent.gameObject.SetActive(false);
            bowLvl2.transform.parent.gameObject.SetActive(false);
            bowLvl3.transform.parent.gameObject.SetActive(false);
            broken = true;
            gameObject.tag = "Broken";
            zabor.SetActive(false);
        }
    }

    IEnumerator Sold1Type()
    {

        for (int k = 0; k < count; k++)
        {
            if (countStatic >= 0 && countStatic < countBricks)
            {
                if (randInt == 0)
                {
                    isBuild = true;
                    towers[0].SetActive(true);
                    upgrade.Play();
                    gameObject.GetComponent<Renderer>().enabled = false;
                    Debug.Log("build");
                    bowLvl1.SetActive(false);
                    spearLvl1.SetActive(false);
                    tower1 = true;
                }
                if (randInt == 1)
                {
                    isBuild = true;
                    towers[0].SetActive(true);
                    upgrade.Play();
                    gameObject.GetComponent<Renderer>().enabled = false;
                    Debug.Log("build");
                    swordLvl1.SetActive(false);
                    spearLvl1.SetActive(false);
                    tower2 = true;
                }
                if (randInt == 2)
                {
                    isBuild = true;
                    towers[0].SetActive(true);
                    upgrade.Play();
                    gameObject.GetComponent<Renderer>().enabled = false;
                    Debug.Log("build");
                    bowLvl1.SetActive(false);
                    swordLvl1.SetActive(false);
                    tower3 = true;
                }
            }

            if (tower1 == true)
            {
                if (countStatic >= countBricks && countStatic < countBricks * 2)
                {

                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(true);
                    bowLvl2.SetActive(false);
                    spearLvl2.SetActive(false);
                    isBuild = false;
                    break;
                }

                if (countStatic >= countBricks * 2 && gameObject.tag != "Built")
                {
                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(false);
                    towers[2].SetActive(true);
                    bowLvl3.SetActive(false);
                    spearLvl3.SetActive(false);
                    gameObject.tag = "Built";
                    isBuild = false;
                    break;
                }
            }

            if (tower2 == true)
            {
                if (countStatic >= countBricks && countStatic < countBricks * 2)
                {

                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(true);
                    swordLvl2.SetActive(false);
                    spearLvl2.SetActive(false);
                    isBuild = false;
                    break;
                }

                if (countStatic >= countBricks * 2 && gameObject.tag != "Built")
                {
                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(false);
                    towers[2].SetActive(true);
                    swordLvl3.SetActive(false);
                    spearLvl3.SetActive(false);
                    gameObject.tag = "Built";
                    isBuild = false;
                    break;
                }
            }

            if (tower3 == true)
            {
                if (countStatic >= countBricks && countStatic < countBricks * 2)
                {

                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(true);
                    bowLvl2.SetActive(false);
                    swordLvl2.SetActive(false);
                    isBuild = false;
                    break;
                }

                if (countStatic >= countBricks * 2 && gameObject.tag != "Built")
                {
                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(false);
                    towers[2].SetActive(true);
                    bowLvl3.SetActive(false);
                    swordLvl3.SetActive(false);
                    gameObject.tag = "Built";
                    isBuild = false;
                    break;
                }
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
            isBuild = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isBuild = false;
        losingBricks.lostBricks = false;
    }
}
