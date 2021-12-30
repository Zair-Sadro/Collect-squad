using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerGen : MonoBehaviour
{
    [SerializeField] LosingBricks losingBricks;
    public int count = 0;
    public GameObject[] towers = new GameObject[3];
    [SerializeField] GameObject buttons;
    [SerializeField] ParticleSystem upgrade;
    [SerializeField] BricksCounter2 brickConter2;
    [SerializeField] GameObject stick;
    [SerializeField] Animator anim;
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
    public GameObject countingBricks;
    public int countBricks = 10;
    public float delay = 3;
    public int countStatic = 0;
    public bool isBuild = false;
    public bool tower1 = false;
    public bool tower2 = false;
    public bool tower3 = false;
    public bool kopilka = false;
    public float hp = 10;
    public int isNeeded = 0;

    private void Start()
    {
        countStatic = -countBricks;
        isNeeded = countBricks;
    }

    void Update()
    {
        if (isBuild)
        {
            //if (countStatic < 0)
            //{
            //    count = brickConter2.plates.Count + losingBricks.howMuch;
            //}
            //if (countStatic >= 0)
            //{
            //    count = brickConter2.plates.Count - losingBricks.howMuch;
            //}
            
            StartCoroutine("Sold1Type");
            
        }

        if (!stick.activeInHierarchy)
        {
            anim.SetBool("Run", false);
        }

        if (!isBuild)
        {
            StopCoroutine("Sold1Type");
        }

        if (kopilka && isBuild)
        {
            //if (count < 0)
            //{
            //    countStatic -= count;
            //}
            //if (count >= 0)
            //{
                //countStatic += count;
            //}
            kopilka = false;
        }

        if (losingBricks.lostBricks == true && count == 0)
        {
            
            losingBricks.lostBricks = false;
            losingBricks.counter = 0;
            brickConter2.heightOffset = brickConter2.saveHeight;
            brickConter2.offset = brickConter2.saveOffset;


            
            losingBricks.lostBricks = false;
        }

        for (int i = 0; i < brickConter2.plates.Count; i++)
        {
            if (brickConter2.plates[i] == null)
            {
                brickConter2.plates.RemoveAt(i);
            }
        }

        if (hp <= 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            bowLvl1.transform.parent.gameObject.SetActive(false);
            bowLvl2.transform.parent.gameObject.SetActive(false);
            bowLvl3.transform.parent.gameObject.SetActive(false);
            zabor.SetActive(false);
        }
    }

    IEnumerator Sold1Type()
    {

        for (int k = 0; k < count; k++)
        {
            if (countStatic >= 0 && countStatic < countBricks && !tower1 && !tower2 && !tower3)
            {
                //countingBricks.SetActive(true);
                //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
                buttons.SetActive(true);
                stick.SetActive(false);
                isBuild = false;
            }

            if (tower1 == true)
            {
                if (countStatic >= countBricks && countStatic < countBricks * 2)
                {

                    upgrade.Play();
                    towers[0].SetActive(false);
                    towers[1].SetActive(true);
                    //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
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
                    //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
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
                    //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
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
                    //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
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
                    //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
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
                    //countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + " / 10";
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

    public void Sword()
    {
        upgrade.Play();
        towers[0].SetActive(true);
        bowLvl1.SetActive(false);
        spearLvl1.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = false;
        isBuild = false;
        tower1 = true;
        print("tower1");
    }

    public void Arrow()
    {
        upgrade.Play();
        towers[0].SetActive(true);
        swordLvl1.SetActive(false);
        spearLvl1.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = false;
        isBuild = false;
        tower2 = true;
    }

    public void Spear()
    {
        upgrade.Play();
        towers[0].SetActive(true);
        bowLvl1.SetActive(false);
        swordLvl1.SetActive(false);
        gameObject.GetComponent<Renderer>().enabled = false;
        isBuild = false;
        tower3 = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Run", false);
            count = brickConter2.plates.Count - losingBricks.howMuch;
            if (count >= isNeeded)
            {
                count = isNeeded;
            }
            isBuild = true;
            kopilka = true;
            countStatic += count;
            if (countStatic >= countBricks * 3)
            {
                isBuild = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isBuild = false;
        count = 0;
        
        if (countStatic < 0)
        {
            isNeeded = (countStatic % countBricks) * -1;
            countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countBricks - ((countStatic % 10) * -1)).ToString() + "/10";
        }


        if (countStatic >= 0)
        {
            isNeeded = countBricks - (countStatic % countBricks);
            countingBricks.GetComponent<TMPro.TextMeshPro>().text = (countStatic % 10).ToString() + "/10";
        }
        if (isNeeded == 0)
        {
            isNeeded = 10;
        }

        
    }
}
