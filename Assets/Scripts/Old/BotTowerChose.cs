using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTowerChose : MonoBehaviour
{
    public List<GameObject> towersBot = new List<GameObject>();
    float rand = 0;
    public int randMain = 0;
    public int bridgeCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChoseTower", 10, 10);
    }

    void ChoseTower()
    {
        rand = Random.Range(0, bridgeCount);
        randMain = System.Convert.ToInt32(rand);
        for (int i = 0; i < towersBot.Count; i++)
        {
            towersBot[i].tag = "Untagged";
            print(towersBot[i].tag);
        }

        towersBot[randMain].tag = "BotTower";
        print("Change");
    }
}
