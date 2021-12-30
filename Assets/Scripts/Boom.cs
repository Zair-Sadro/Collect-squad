using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] GameObject kucha;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] PlayerTowerGen generation;
    [SerializeField] GameObject bowLvl1;
    [SerializeField] GameObject spearLvl1;
    [SerializeField] GameObject swordLvl1;
    [SerializeField] GameObject bowLvl2;
    [SerializeField] GameObject spearLvl2;
    [SerializeField] GameObject swordLvl2;
    [SerializeField] GameObject bowLvl3;
    [SerializeField] GameObject spearLvl3;
    [SerializeField] GameObject swordLvl3;
    public int vibrating = 0;

    Vector3 startPosition;
    private void Start()
    {
        startPosition = gameObject.transform.position;
        if (PlayerPrefs.HasKey("Vibration"))
        {
            vibrating = PlayerPrefs.GetInt("Vibration");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower1"))
        {
            other.gameObject.SetActive(false);
            smoke.Play();
            gameObject.SetActive(false);
            gameObject.transform.position = startPosition;
            kucha.GetComponent<Renderer>().enabled = true;
            generation.countStatic = -generation.countBricks;
            generation.countingBricks.SetActive(false);
            bowLvl1.SetActive(true);
            spearLvl1.SetActive(true);
            swordLvl1.SetActive(true);
            generation.tower1 = false;
            generation.tower2 = false;
            generation.tower3 = false;
            if (vibrating == 1)
            {
                Vibration.Vibrate(100);
            }
        }

        if (other.CompareTag("Tower2"))
        {
            other.gameObject.SetActive(false);
            smoke.Play();
            gameObject.SetActive(false);
            gameObject.transform.position = startPosition;
            kucha.GetComponent<Renderer>().enabled = true;
            generation.countStatic = -generation.countBricks;
            generation.countingBricks.SetActive(false);
            bowLvl1.SetActive(true);
            spearLvl1.SetActive(true);
            swordLvl1.SetActive(true);
            bowLvl2.SetActive(true);
            spearLvl2.SetActive(true);
            swordLvl2.SetActive(true);
            generation.tower1 = false;
            generation.tower2 = false;
            generation.tower3 = false;
            if (vibrating == 1)
            {
                Vibration.Vibrate(25);
            }
        }

        if (other.CompareTag("Tower3"))
        {
            other.gameObject.SetActive(false);
            smoke.Play();
            gameObject.SetActive(false);
            gameObject.transform.position = startPosition;
            kucha.GetComponent<Renderer>().enabled = true;
            kucha.tag = "PlayerTower";
            generation.countStatic = -generation.countBricks;
            generation.countingBricks.SetActive(false);
            bowLvl1.SetActive(true);
            spearLvl1.SetActive(true);
            swordLvl1.SetActive(true);
            bowLvl2.SetActive(true);
            spearLvl2.SetActive(true);
            swordLvl2.SetActive(true);
            bowLvl3.SetActive(true);
            spearLvl3.SetActive(true);
            swordLvl3.SetActive(true);
            generation.tower1 = false;
            generation.tower2 = false;
            generation.tower3 = false;
            if (vibrating == 1)
            {
                Vibration.Vibrate(25);
            }
        }
    }
}
