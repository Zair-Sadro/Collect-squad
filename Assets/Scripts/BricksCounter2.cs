using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BricksCounter2 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Text counterText;
    public List<GameObject> plates = new List<GameObject>();
    [Range(0, 100)]
    public float offset;
    public int platesInRaw = 2;
    public int heightOffset = 0;
    int bricksCount = 0;
    public float saveOffset = 0;
    public int saveHeight = 0;
    public int vibrating = 0;

    private void Start()
    {
        saveHeight = heightOffset;
        saveOffset = offset;
        bricksCount = 50;
        if (PlayerPrefs.HasKey("Vibration"))
        {
            vibrating = PlayerPrefs.GetInt("Vibration");
        }
    }

    private void Update()
    {
        counterText.text = plates.Count.ToString();
    }

    IEnumerator SetPosition(GameObject plate)
    {
        plate.transform.SetParent(player.transform);
        yield return new WaitForFixedUpdate();
        if (plates.Count % platesInRaw == 0)
        {
            plate.transform.localPosition = new Vector3(0, heightOffset * plates.Count, 0);
        }

        else
        {
            plate.transform.localPosition = new Vector3(0, heightOffset * (plates.Count - 1), offset);
        }

        plate.transform.rotation = new Quaternion(0, 0, 0, 0);
        plate.transform.localScale = new Vector3(5000, 3000, 7000);
        plates.Add(plate.gameObject);
        if (vibrating == 1)
        {
            Vibration.Vibrate(50);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerPlate") && plates.Count < bricksCount)
        {
            StartCoroutine(SetPosition(other.gameObject));
            
        }
    }
}
