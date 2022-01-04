using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBrickCounter : MonoBehaviour
{
    [SerializeField] GameObject bot;
    public List<GameObject> plates = new List<GameObject>();
    [Range(0, 100)]
    public float offset;
    public int platesInRaw = 2;
    public int heightOffset = 0;
    public bool taken;
    public int bricksCount = 0;
    public float saveOffset = 0;
    public int saveHeight = 0;


    private void Awake()
    {
        bricksCount = Random.Range(3, 11);
        saveOffset = offset;
        saveHeight = heightOffset;
    }

    //private void Update()
    //{
    //    if (plates.Count != 0)
    //    {
    //        taken = true;
    //    }
    //}

    IEnumerator SetPosition(GameObject plate)
    {
        taken = true;
        plate.transform.SetParent(bot.transform);
        yield return new WaitForFixedUpdate();
        if (plates.Count % platesInRaw == 0)
        {
            plate.transform.localPosition = new Vector3(0, heightOffset * plates.Count, 0);
        }

        else
        {
            plate.transform.localPosition = new Vector3(0, heightOffset * (plates.Count - 1), -offset);
        }

        plate.transform.rotation = new Quaternion(0, 0, 0, 0);
        plate.transform.localScale = new Vector3(5000, 3000, 7000);
        plates.Add(plate.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EnemyPlate") && plates.Count < bricksCount)
        {
            StartCoroutine(SetPosition(other.gameObject));
            taken = true;
        }
    }
}
